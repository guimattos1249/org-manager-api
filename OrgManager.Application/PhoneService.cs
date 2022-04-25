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
    public class PhoneService : IPhoneService
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IMapper _mapper;

        public PhoneService(
            IPhoneRepository phoneRepository,
            IMapper mapper
        )
        {
            _phoneRepository = phoneRepository;
            _mapper = mapper;
        }

        public async Task<PhoneDto> AddPhone(int userId, int organizationId, PhoneDto model)
        {
            try
            {
                Phone phone = _mapper.Map<Phone>(model);
            
                if(userId != 0)
                    phone.UserId = userId;

                if(organizationId != 0)
                    phone.OrganizationId = organizationId;

                _phoneRepository.Add<Phone>(phone);
                if(await _phoneRepository.SaveChangesAsync())
                {
                    Phone phoneReturn = await _phoneRepository.GetPhoneByIdAsync(userId, organizationId, phone.Id);
                    return _mapper.Map<PhoneDto>(phoneReturn);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<PhoneDto> UpdatePhone(int userId, int organizationId, PhoneDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeletePhone(int userId, int organizationId, int phoneId)
        {
            try
            {            
                Phone phoneToDelete = await _phoneRepository.GetPhoneByIdAsync(userId, organizationId, phoneId);
                if(phoneToDelete == null) return false;
                _phoneRepository.Delete<Phone>(phoneToDelete);
                return await _phoneRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<PhoneDto> GetPhoneByIdAsync(int userId, int organizationId, int phoneId)
        {
            throw new NotImplementedException();
        }
    }
}