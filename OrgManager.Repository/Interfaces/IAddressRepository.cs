using OrgManager.Domain;

namespace OrgManager.Repository.Interfaces
{
    public interface IAddressRepository : IGeneralRepository
    {
        Task<Address> GetAddressByIdsAsync(int organizationId, int addressId, int userId = 0);
        Task<Address[]> GetAllByAddressesIdAsync(int userId, int organizationId);
    }
}