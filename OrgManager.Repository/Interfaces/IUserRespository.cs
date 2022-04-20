using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrgManager.Domain.Identity;

namespace OrgManager.Repository.Interfaces
{
    public interface IUserRespository : IGeneralRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int Id);
        Task<User> GetUserByUserNameAsync(string userName);
    }
}