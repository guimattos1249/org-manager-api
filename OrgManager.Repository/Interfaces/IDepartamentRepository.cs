using OrgManager.Domain;
using OrgManager.Domain.Identity;
using OrgManager.Repository.Helpers;

namespace OrgManager.Repository.Interfaces
{
    public interface IDepartamentRepository : IGeneralRepository
    {
        Task<Departament> GetDepartamentByIdAsync(int departamentId, int organizationId);
        Task<Departament> GetAllUsersInDepartamentAsync(int departamentId, int organizationId);
        Task<User> GetAllDepartamentsInUserAsync(int userId);
        Task<Departament[]> GetAllDepartamentsInOrganizationAsync(int organizationId);
        Task<Departament[]> GetAllDepartamentsWithAllUsersInOrganizationAsync(int organizationId);
    }
}