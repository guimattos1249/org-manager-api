using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OrgManager.Application.Dtos;
using OrgManager.Domain;
using OrgManager.Repository.Interfaces;
using src.OrgManager.Application.Interfaces;
namespace src.OrgManager.Application
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressService(
            IAddressRepository addressRepository,
            IMapper mapper
        )
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<AddressDto> AddAddress(int userId, int organizationId, AddressDto model)
        {
            try
            {
                Address address = _mapper.Map<Address>(model);
            
                if(userId != 0)
                    address.UserId = userId;

                if(organizationId != 0)
                    address.OrganizationId = organizationId;

                _addressRepository.Add<Address>(address);
                if(await _addressRepository.SaveChangesAsync())
                {
                    Address addressReturn = await _addressRepository.GetAddressByIdsAsync(organizationId, address.Id, userId);
                    return _mapper.Map<AddressDto>(addressReturn);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AddressDto> UpdateAddress(int userId, int organizationId, AddressDto model)
        {
            try
            {
                Address address = await _addressRepository.GetAddressByIdsAsync(organizationId, model.Id, userId);
                if(address == null) return null;
                if(organizationId != 0) model.OrganizationId = organizationId;
                if(userId != 0) model.UserId = userId;
                _addressRepository.Update<Address>(_mapper.Map<Address>(model));
                if(await _addressRepository.SaveChangesAsync())
                {
                    Address addressReturn = await _addressRepository.GetAddressByIdsAsync(organizationId, address.Id, userId);

                    return _mapper.Map<AddressDto>(addressReturn);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAddress(int userId, int organizationId, int addressId)
        {
            try
            {            
                Address addressToDelete = await _addressRepository.GetAddressByIdsAsync(organizationId, addressId, userId);
                if(addressToDelete == null) return false;
                _addressRepository.Delete<Address>(addressToDelete);
                return await _addressRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AddressDto> GetAddressByIdAsync(int userId, int organizationId, int addressId)
        {
            try
            {
                Address Address = await _addressRepository.GetAddressByIdsAsync(organizationId, addressId, userId);
                if(Address == null) return null;

                return _mapper.Map<AddressDto>(Address);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AddressDto[]> GetAllAddressesByIdAsync(int userId, int organizationId)
        {
            try
            {
                Address[] Addresses = await _addressRepository.GetAllByAddressesIdAsync(userId, organizationId);
                if(Addresses == null) return null;

                return _mapper.Map<AddressDto[]>(Addresses);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}