using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrgManager.Domain;
using OrgManager.Repository.Contexts;
using OrgManager.Repository.Helpers;
using OrgManager.Repository.Interfaces;

namespace OrgManager.Repository
{
    public class DepartamentRepository : GeneralRepository, IDepartamentRepository
    {
        private readonly OrgManagerContext _context;

        public DepartamentRepository(OrgManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Departament> GetDepartamentByIdsAsync(int organizationId, int departamentId)
        {
            IQueryable<Departament> query = _context.Departaments;

            query = query.AsNoTracking()
                         .Where(dp => dp.OrganizationId == organizationId &&
                                      dp.Id == departamentId);
                                      
            return await query.FirstOrDefaultAsync();
        }

        public async Task<PageList<Departament>> GetAllByDepartamentesIdsAsync(PageParams pageParams, int organizationId, int departamentId)
        {
            IQueryable<Departament> query = _context.Departaments;

            query = query.AsNoTracking()
                         .Where(o => o.Name.ToLower().Contains(pageParams.Term.ToLower()))
                         .OrderBy(o => o.Id);

            return await PageList<Departament>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        //TODO: Create UserDepartament
    }
}