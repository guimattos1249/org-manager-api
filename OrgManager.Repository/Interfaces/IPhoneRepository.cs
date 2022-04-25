using OrgManager.Domain;

namespace OrgManager.Repository.Interfaces
{
    public interface IPhoneRepository : IGeneralRepository
    {
        Task<Phone> GetPhoneByIdAsync(int userId, int organizationId, int phoneId);
        Task<Phone[]> GetAllPhonesByIdAsync(int userId, int organizationId);
    }
}