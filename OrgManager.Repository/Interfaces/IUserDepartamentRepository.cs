using OrgManager.Domain;
using OrgManager.Domain.Identity;
using OrgManager.Repository.Helpers;

namespace OrgManager.Repository.Interfaces
{
    public interface IUserDepartamentRepository : IGeneralRepository
    {
        Task<UserDepartament> GetUserDepartamentByIdsAsync(int userId, int departamentId, int organizationId);
        Task<UserDepartament> GetUserDepartamentByDepartamentIdAsync(int departamentId, int organizationId);
        Task<UserDepartament> GetUserDepartamentByUserIdAsync(int userId);
    }
}