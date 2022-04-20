using OrgManager.Domain;

namespace OrgManager.Repository.Interfaces
{
    public interface IPhoneRepository
    {
        Task<Phone> GetPhoneUserByIdsAsync(int userId, int id);
        Task<Phone> GetPhoneOrganizationByIdsAsync(int organizationId, int id);
        Task<Phone[]> GetAllByUserIdsAsync(int userId);
        Task<Phone[]> GetAllByOrganizationIdsAsync(int organizationId);
    }
}