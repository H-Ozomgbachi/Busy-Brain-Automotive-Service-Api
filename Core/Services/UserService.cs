using Common.Contracts.Exceptions;
using Common.Contracts.Exceptions.Types;
using Common.Contracts.v1.Account;
using Common.Core.CQRS.Commands;
using Common.Core.CQRS.Commands.User;
using Common.Core.CQRS.Queries;
using Common.Core.CQRS.Queries.User;
using Common.Core.Services.EmailManager;
using Common.Core.Services.Validation;
using Common.Data.Domain;
using EmailService;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Common.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly IOrganisationService _organisationService;
        private readonly IEmailSender _emailSender;

        public UserService(IMediator mediator, IConfiguration configuration, IOrganisationService organisationService, IEmailSender emailSender)
        {
            _mediator = mediator;
            _config = configuration;
            _organisationService = organisationService;
            _emailSender = emailSender;
        }
        
        public async Task<AuthenticateResponseModel> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new UnauthorizedAccessException("username and/or password is incorrect");
            }

            // check if username exists
            var user = await _mediator.Send(new GetUserByUsernameQuery() { Username = username });

            if (user == null)
            {
                throw new UnauthorizedAccessException("username and/or password is incorrect");
            }

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new UnauthorizedAccessException("username and/or password is incorrect");
            }

            // authentication successful
            return new AuthenticateResponseModel()
            {
                Id = user.Id,
                Email = user.Email,
                CountryCode = user.CountryCode,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Roles = user.Roles,
                OrganisationId = user.OrganisationId,
                PositionInOrganisation = user.PositionInOrganisation,
                RecordId = user.RecordId,
                Token = generateJwtToken(user)
            };
        }

        public Task<IEnumerable<UserModel>> GetAll(int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> GetById(int id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery() { ID = id });

            if(user == null) {

                throw new BusinessLogicException($"requested user was not found:{id}", "requested user was not found");
            }

            return new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Roles = user.Roles,
                OrganisationId = user.OrganisationId,
                CountryCode = user.CountryCode,
                RecordId = user.RecordId,
                PositionInOrganisation = user.PositionInOrganisation
            };
        }

        public async Task<UserModel> GetById(Guid id)
        {
            var user = await _mediator.Send(new GetUserByGuidQuery() { ID = id });

            if (user == null)
            {
                throw new BusinessLogicException($"requested user was not found:{id}", "requested user was not found");
            }

            return new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Roles = user.Roles,
                OrganisationId = user.OrganisationId,
                PositionInOrganisation = user.PositionInOrganisation,
                CountryCode = user.CountryCode,
                RecordId = user.RecordId
            };
        }

        public async Task<UserModel> Create(RegisterUserModel user, Guid currentUser)
        {
            RegisterUserValidator validator = new RegisterUserValidator();

            ValidationResult results = validator.Validate(user);

            if (!results.IsValid)
            {
                var ex = new BusinessLogicException("registration validation error", "account registration validation error");
                var validationErrors = new List<ValidationError>();
                foreach (var failure in results.Errors)
                {
                    validationErrors.Add(new ValidationError(failure.PropertyName, failure.ErrorMessage));
                }
                ex.ValidationErrors = validationErrors;
                throw ex;
            }

            //Does username/email exist
            var doesEmailExist = await _mediator.Send(new DoesUserEmailExistQuery() { Email = user.Email, ExcludeUserId = Guid.Empty });

            if (doesEmailExist)
            {
                throw new BusinessLogicException($"An account already exists with email: {user.Email}", $"An account already exists with email: {user.Email}");
            }
            var organisation = await _organisationService.Create(user.Organisation, currentUser);

            if(organisation == null)
            {
                throw new BusinessLogicException($"Could not create an organisation", $"Could not create an organisation");
            }

            int position = await _mediator.Send(new CountUsersInOrganisationQuery { OrganisationId = organisation.ID });

            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUserId = await _mediator.Send(new RegisterUserCommand() {
                FirstName  = user.FirstName,
                LastName = user.LastName,                
                Email = user.Email,
                Phone = user.Phone,
                CountryCode = user.CountryCode,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Roles = new List<string>{"Customer"},
                ForcePasswordReset = false,
                PasswordResetToken = Guid.NewGuid(),
                CreatedBy = currentUser,
                OrganisationId = organisation.ID,
                PositionInOrganisation = position + 1
            });

            if(newUserId > 0)
            {
                return await GetById(newUserId);

            }
            else
            {
                throw new BusinessLogicException($"An account already exists with email: {user.Email}", $"An account already exists with email: {user.Email}");
            }
        }

        public async Task<UserModel> CreateOrganisationUser(AddUserToOrganisationModel user, Guid currentUser)
        {
            AddUserToOrganisationValidator validator = new AddUserToOrganisationValidator();

            ValidationResult results = validator.Validate(user);

            if (!results.IsValid)
            {
                var ex = new BusinessLogicException("registration validation error", "account registration validation error");
                var validationErrors = new List<ValidationError>();
                foreach (var failure in results.Errors)
                {
                    validationErrors.Add(new ValidationError(failure.PropertyName, failure.ErrorMessage));
                }
                ex.ValidationErrors = validationErrors;
                throw ex;
            }

            //Does username/email exist
            var doesEmailExist = await _mediator.Send(new DoesUserEmailExistQuery() { Email = user.Email, ExcludeUserId = Guid.Empty });

            if (doesEmailExist)
            {
                throw new BusinessLogicException($"An account already exists with email: {user.Email}");
            }
            var organisation = await _organisationService.GetById(user.OrganisationId);

            if (organisation == null)
            {
                throw new BusinessLogicException($"Could not find your organisation");
            }
            int position = await _mediator.Send(new CountUsersInOrganisationQuery { OrganisationId = organisation.ID });
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUserId = await _mediator.Send(new RegisterUserCommand()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                CountryCode = user.CountryCode,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Roles = new List<string> { "Customer" },
                ForcePasswordReset = false,
                PasswordResetToken = Guid.NewGuid(),
                CreatedBy = currentUser,
                OrganisationId = organisation.ID,
                PositionInOrganisation = position + 1

            });

            if (newUserId > 0)
            {
                return await GetById(newUserId);
            }
            else
            {
                throw new BusinessLogicException($"An account already exists with email: {user.Email}");
            }
        }

        public async Task<UserModel> UpdateAsync(UpdateUserModel updateUser, Guid currentUser)
        {
            UpdateUserValidator validator = new UpdateUserValidator();

            ValidationResult results = validator.Validate(updateUser);

            if (!results.IsValid)
            {
                var ex = new BusinessLogicException("Account update validation error", "account update validation error");
                var validationErrors = new List<ValidationError>();
                foreach (var failure in results.Errors)
                {
                    validationErrors.Add(new ValidationError(failure.PropertyName, failure.ErrorMessage));
                }
                ex.ValidationErrors = validationErrors;
                throw ex;
            }

            var loggedInUser = await GetById(currentUser);

            if (!string.IsNullOrWhiteSpace(updateUser.Email))
            {
                //Does username/email exist
                var doesEmailExist = await _mediator.Send(new DoesUserEmailExistQuery() { Email = updateUser.Email, ExcludeUserId = updateUser.UserId });

                if (doesEmailExist && updateUser.Email != loggedInUser.Email)
                {
                    throw new BusinessLogicException($"An account already exists with email: {updateUser.Email}");
                }
            }

            byte[] passwordHash = null, passwordSalt = null;

            if (!string.IsNullOrWhiteSpace(updateUser.Email))
            {
                CreatePasswordHash(updateUser.Password, out passwordHash, out passwordSalt);
            }

            var newUserId = await _mediator.Send(new UpdateUserCommand()
            {
                ChangedBy = currentUser,
                FirstName = updateUser.FirstName,
                LastName = updateUser.LastName,
                Email = updateUser.Email,
                Phone = updateUser.Phone,
                CountryCode = updateUser.CountryCode,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserId =  updateUser.UserId,
                ForcePasswordReset = updateUser.ForcePasswordReset,
                Status = updateUser.Status,
                IsDeleted = updateUser.IsDeleted,
                Roles = updateUser.Roles,
                OrganisationId = updateUser.OrganisationId,
            });

            if (newUserId > 0)
            {
                return await GetById(newUserId);
            }
            else
            {
                throw new BusinessLogicException($"An account already exists with email: {updateUser.Email}");
            }
        }

        public async Task<bool> CanUserAccessOrganizationAsync(Guid userId, int orgId)
        {
            var hasAccess = await _mediator.Send(new DoesUserBelongToOrganisationQuery() { UserId = userId, OrganizationId =orgId});
            return hasAccess;
        }

        public async Task<UserModel> ModifyUserRole(Guid userId, string[] newRoles)
        {
            var databaseResponse = await _mediator.Send(new ModifyUserRoleCommand
            {
                UniqueId = userId,
                Roles = newRoles
            });

            if (databaseResponse <= 0)
            {
                throw new BusinessLogicException("Could not modify user's role");
            }

            return await GetById(userId);
        }
        // private helper methods
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("JwtToken:SigningKey"));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.UserData, user.OrganisationId.HasValue ? user.OrganisationId.ToString() : string.Empty)
            };
            // Add roles as multiple claims
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Audience = _config.GetValue<string>("JwtToken:Audience"),
                Issuer = _config.GetValue<string>("JwtToken:Issuer"),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task SendPasswordResetLink(string email)
        {
            var userData = await _mediator.Send(new GetUserByUsernameQuery { Username = email });
            if (userData is null)
            {
                throw new BusinessLogicException("Email does not exist", "Email does not exist");
            }
            
            await _emailSender.SendEmail(EmailTemplates.PasswordResetEmail(userData));
        }

        public async Task ResetPassword(ResetPasswordModel resetPasswordModel, Guid currentUser)
        {
            var userData = await _mediator.Send(new GetUserByGuidQuery { ID = resetPasswordModel.UserGuid});
            if (userData is null || (userData.PasswordResetToken != resetPasswordModel.ResetToken))
            {
                throw new BusinessLogicException("Request was mutilated", "Request was mutilated");
            }

            UpdateUserModel updatedUser = new UpdateUserModel() 
            { 
                UserId = userData.Id,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Email = userData.Email,
                Phone = userData.Phone,
                CountryCode = userData.CountryCode,
                Password = resetPasswordModel.NewPassword,
                Roles = userData.Roles,
            };
            await UpdateAsync(updatedUser, currentUser);
        }
    }
}
