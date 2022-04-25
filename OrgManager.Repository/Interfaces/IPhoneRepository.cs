using OrgManager.Domain;

namespace OrgManager.Repository.Interfaces
{
    public interface IPhoneRepository : IGeneralRepository
    {
        Task<Phone> GetPhoneByIdAsync(int userId, int organizationId, int phoneId);
        Task<Phone[]> GetAllByUserIdsAsync(int userId);
        Task<Phone[]> GetAllByOrganizationIdsAsync(int organizationId);
    }
}