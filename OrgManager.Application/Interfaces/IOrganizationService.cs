using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrgManager.Application.Dtos;
using OrgManager.Repository.Helpers;

namespace OrgManager.Application.Interfaces
{
    public interface IOrganizationService
    {
        Task<OrganizationDto> AddOrganization(OrganizationDto model);
        Task<OrganizationDto> UpdateOrganization(OrganizationDto model);
        Task<bool> DeleteOrganization(int organizationId);

        Task<OrganizationDto> GetOrganizationByIdAsync(int organizationId);
    }
}