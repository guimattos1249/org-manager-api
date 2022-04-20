using OrgManager.Domain;
using OrgManager.Repository.Helpers;

namespace OrgManager.Repository.Interfaces
{
    public interface IDepartamentRepository : IGeneralRepository
    {
        Task<Departament> GetDepartamentByIdsAsync(int organizationId, int departamentId);
        Task<PageList<Departament>> GetAllByDepartamentesIdsAsync(PageParams pageParams, int organizationId, int departamentId);
    }
}