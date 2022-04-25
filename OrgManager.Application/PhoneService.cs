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

        public async Task<PhoneDto> UpdatePhone(int userId, int organizationId, PhoneDto model)
        {
            try
            {
                Phone phoneToUpdate = await _phoneRepository.GetPhoneByIdAsync(userId, organizationId, model.Id);
                if(phoneToUpdate == null) return null;
                if(organizationId != 0) model.OrganizationId = organizationId;
                if(userId != 0) model.UserId = userId;
                _phoneRepository.Update<Phone>(_mapper.Map<Phone>(model));
                if(await _phoneRepository.SaveChangesAsync())
                {
                    Phone phoneReturn = await _phoneRepository.GetPhoneByIdAsync(userId, organizationId, model.Id);

                    return _mapper.Map<PhoneDto>(phoneReturn);
                }
                return null;
            }
            catch (System.Exception)
            {
                
                throw;
            }
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

        public async Task<PhoneDto> GetPhoneByIdAsync(int userId, int organizationId, int phoneId)
        {
            try
            {
                Phone phone = await _phoneRepository.GetPhoneByIdAsync(userId, organizationId, phoneId);
                if(phone == null) return null;

                return _mapper.Map<PhoneDto>(phone);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PhoneDto[]> GetAllPhonesByIdAsync(int userId, int organizationId)
        {
            try
            {
                Phone[] phones = await _phoneRepository.GetAllPhonesByIdAsync(userId, organizationId);
                if(phones == null) return null;

                return _mapper.Map<PhoneDto[]>(phones);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}