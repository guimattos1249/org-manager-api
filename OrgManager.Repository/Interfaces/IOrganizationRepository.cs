using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrgManager.Domain;
using OrgManager.Repository.Helpers;

namespace OrgManager.Repository.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<PageList<Organization>> GetAllOrganizationsAsync(PageParams pageParams);
        Task<Organization> GetOrganizationByIdAsync(int Id);
    }
}