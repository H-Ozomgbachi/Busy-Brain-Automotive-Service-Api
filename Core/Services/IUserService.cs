using Common.Contracts.v1.Account;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Core.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponseModel> AuthenticateAsync(string username, string password);
        Task<IEnumerable<UserModel>> GetAll(int pageSize, int pageNumber);
        Task<UserModel> GetById(int id);
        Task<UserModel> GetById(Guid id);
        Task<UserModel> Create(RegisterUserModel user, Guid currentUser);
        Task<UserModel> UpdateAsync(UpdateUserModel user, Guid currentUser);
        Task<bool> CanUserAccessOrganizationAsync(Guid userId, int orgId);
        Task<UserModel> CreateOrganisationUser(AddUserToOrganisationModel user, Guid currentUser);
        Task<UserModel> ModifyUserRole(Guid userId, string[] newRoles);
        Task SendPasswordResetLink(string email);
        Task ResetPassword(ResetPasswordModel resetPasswordModel, Guid currentUser);

    }
}
