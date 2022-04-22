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
                var organization = _mapper.Map<Organization>(model);
                _organizationRepository.Add<Organization>(organization);
                if(await _organizationRepository.SaveChangesAsync())
                {
                    var organizationReturn = _organizationRepository.GetOrganizationByIdAsync(organization.Id);

                    return _mapper.Map<OrganizationDto>(organizationReturn);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<OrganizationDto> UpdateOrganization(int userId, int OrganizationId, OrganizationDto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOrganization(int userId, int OrganizationId)
        {
            throw new NotImplementedException();
        }

        public Task<OrganizationDto> GetOrganizationByIdAsync(int userId, int OrganizationId)
        {
            //TODO : To get organization, is needed verify if the user existis in this organization
            throw new NotImplementedException();
        }
    }
}