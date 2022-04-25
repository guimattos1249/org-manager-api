using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrgManager.Application.Dtos;

namespace src.OrgManager.Application.Interfaces
{
    public interface IAddressService
    {
        Task<AddressDto> AddAddress(int userId, int organizationId, AddressDto model);
        Task<AddressDto> UpdateAddress(int userId, int organizationId, AddressDto model);
        Task<bool> DeleteAddress(int userId, int organizationId, int AddressId);

        Task<AddressDto> GetAddressByIdAsync(int userId, int organizationId, int addressId);
        Task<AddressDto[]> GetAllAddressesByIdAsync(int userId, int organizationId);
    }
}