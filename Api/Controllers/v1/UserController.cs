using Common.Api.Configuration.Authorization;
using Common.Contracts.Exceptions.Types;
using Common.Contracts.v1.Account;
using Common.Core.CQRS.Queries.User;
using Common.Core.Models;
using Common.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Common.Api.Controllers.v1
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public UserController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Supervisor")]
        public async Task<List<ListUserViewModel>> GetAllUsers()
        {
            return await _mediator.Send(new GetUsersQuery());
        }

        [HttpGet("organisations/{id}")]
        public async Task<List<ListUserViewModel>> GetUsersInOrganisation(int id)
        {
            return await _mediator.Send(new GetUsersInOrganisationQuery() {OrganisationId = id });
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Customer,Admin,Manager,Supervisor,User")]
        public async Task<UserModel> GetAsync(Guid id)
        {
            string strUserId = User.FindFirstValue(ClaimTypes.Name);

            var activeUserId = Guid.Parse(strUserId);

            IEnumerable<Claim> roleClaims = User.FindAll(ClaimTypes.Role);

            IEnumerable<string> roles = roleClaims.Select(r => r.Value);

            if (roles.Contains(Roles.Customer) || roles.Contains(Roles.User))
            {
                if (activeUserId != id)
                {
                    throw new BusinessLogicException($"User ids do not match {id} vs {activeUserId}", $"You don not have permission to view another user account");
                }
            }
            return await _userService.GetById(id);
        }
        
        [HttpPost("authenticate")]
        public async Task<AuthenticateResponseModel> PostAsync([FromBody] AuthenticateModel model)
        {
            return await _userService.AuthenticateAsync(model.Username, model.Password);
        }


        [HttpPost("register")]
        public async Task<UserModel> PostAsync([FromBody] RegisterUserModel model)
        {
            return await _userService.Create(model,Guid.Empty);
        }

        [HttpPost("add-user-to-org")]
        public async Task<UserModel> AddUserToOrg([FromBody] AddUserToOrganisationModel model)
        {
            return await _userService.CreateOrganisationUser(model, Guid.Empty);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Customer,Admin,Manager,Supervisor,User")]
        public async Task<UserModel> PutAsync(Guid id, [FromBody] UpdateUserModel model)
        {
            string strUserId = User.FindFirstValue(ClaimTypes.Name);

            var activeUserId = Guid.Parse(strUserId);

            if(User.IsInRole(Roles.Customer) || User.IsInRole(Roles.User))
            {
                if (id != model.UserId || activeUserId !=  id || activeUserId != model.UserId)
                {
                    throw new BusinessLogicException($"User ids do not match {id} vs {model.UserId} vs {activeUserId}", $"You don not have permission to edit another user account");
                }
            }

           return await _userService.UpdateAsync(model, activeUserId);
        }

        [HttpPatch("{id}/modify-role")]
        [Authorize(Roles = "Admin")]
        public async Task<UserModel> ModifyUserRole(Guid id, string[] roles)
        {
            return await _userService.ModifyUserRole(id, roles);
        }

        [HttpGet("forgot-password/{username}")]
        public async Task<IActionResult> ForgotPassword(string username)
        {
            await _userService.SendPasswordResetLink(username);
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel payload)
        {
            await _userService.ResetPassword(payload, payload.UserGuid);
            return Ok();
        }
    }
}
