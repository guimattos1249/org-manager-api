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
        Task<OrganizationDto> AddOrganizations(OrganizationDto model);
        Task<OrganizationDto> UpdateOrganization(int userId, int OrganizationId, OrganizationDto model);
        Task<bool> DeleteOrganization(int userId, int OrganizationId);

        Task<OrganizationDto> GetOrganizationByIdAsync(int userId, int OrganizationId);
    }
}