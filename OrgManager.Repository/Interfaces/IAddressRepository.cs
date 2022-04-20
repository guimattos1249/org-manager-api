using OrgManager.Domain;

namespace OrgManager.Repository.Interfaces
{
    public interface IAddressRepository : IGeneralRepository
    {
        Task<Address> GetAddressUserByIdsAsync(int userId, int id);
        Task<Address> GetAddressOrganizationByIdsAsync(int organizationId, int id);
        Task<Address[]> GetAllByUserIdsAsync(int userId);
        Task<Address[]> GetAllByOrganizationIdsAsync(int organizationId);
    }
}