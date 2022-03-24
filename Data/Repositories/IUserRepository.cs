using Common.Data.Domain;
using System;
using System.Threading.Tasks;

namespace Common.Data.Repositories
{
    public interface  IUserRepository
    {
        Task<int> CreateAsync(User user, Guid changedby);
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByRecordIdAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task<int> UpdateAsync(User user, Guid changedby);
        Task<bool> DoesUsernameExistAsync(string username,Guid exclude);
        Task<int> ModifyRoleAsync(string[] roles, Guid currentUser);
    }
}
