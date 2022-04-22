using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OrgManager.Application.Dtos;
using OrgManager.Application.Interfaces;
using OrgManager.Domain;
using OrgManager.Repository.Helpers;
using OrgManager.Repository.Interfaces;

namespace OrgManager.Application
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public OrganizationService(
            IOrganizationRepository organizationRepository,
            IMapper mapper
        )
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }

        public async Task<OrganizationDto> AddOrganizations(OrganizationDto model)
        {
            try
            {
                Organization organization = _mapper.Map<Organization>(model);
                _organizationRepository.Add<Organization>(organization);
                if(await _organizationRepository.SaveChangesAsync())
                {
                    Organization organizationReturn = await _organizationRepository.GetOrganizationByIdAsync(organization.Id);

                    return _mapper.Map<OrganizationDto>(organizationReturn);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrganizationDto> UpdateOrganization(OrganizationDto model)
        {
            //TODO : To update organization, is needed verify if the user existis in this organization
            try
            {
                Organization organization = await _organizationRepository.GetOrganizationByIdAsync(model.Id);
                if (organization == null) return null;
                _organizationRepository.Update<Organization>(organization);
                if(await _organizationRepository.SaveChangesAsync())
                {
                    Organization organizationReturn = await _organizationRepository.GetOrganizationByIdAsync(organization.Id);

                    return _mapper.Map<OrganizationDto>(organizationReturn);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteOrganization(int organizationId)
        {
            try
            {
                Organization organization =
                await _organizationRepository.GetOrganizationByIdAsync(organizationId);
                if (organization == null) return false;
                _organizationRepository.Delete<Organization>(organization);
                return await _organizationRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrganizationDto> GetOrganizationByIdAsync(int organizationId)
        {
            //TODO : To get organization, is needed verify if the user existis in this organization
            try
            {
                var organization = await _organizationRepository.GetOrganizationByIdAsync(organizationId);
                if (organization == null) return null;

                var resultado = _mapper.Map<OrganizationDto>(organization);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}