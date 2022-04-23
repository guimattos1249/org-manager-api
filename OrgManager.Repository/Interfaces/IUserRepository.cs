using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrgManager.Domain.Identity;

namespace OrgManager.Repository.Interfaces
{
    public interface IUserRepository : IGeneralRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByIdInOrganizationAsync(int id, int organizationId);
        Task<User> GetUserByUserNameAsync(string userName);
    }
}