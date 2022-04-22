using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrgManager.Domain;
using OrgManager.Repository.Helpers;

namespace OrgManager.Repository.Interfaces
{
    public interface IOrganizationRepository : IGeneralRepository
    {
        Task<Organization> GetOrganizationByIdAsync(int Id);
    }
}