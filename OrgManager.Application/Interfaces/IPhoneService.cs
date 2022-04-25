using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrgManager.Application.Dtos;

namespace src.OrgManager.Application.Interfaces
{
    public interface IPhoneService
    {
        Task<PhoneDto> AddPhone(int userId, int organizationId, PhoneDto model);
        Task<PhoneDto> UpdatePhone(int userId, int organizationId, PhoneDto model);
        Task<bool> DeletePhone(int userId, int organizationId, int phoneId);

        Task<PhoneDto> GetPhoneByIdAsync(int userId, int organizationId, int phoneId);
        Task<PhoneDto[]> GetAllPhonesByIdAsync(int userId, int organizationId);
    }
}