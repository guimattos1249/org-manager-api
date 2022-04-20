using Microsoft.EntityFrameworkCore;
using OrgManager.Domain;
using OrgManager.Repository.Contexts;
using OrgManager.Repository.Helpers;
using OrgManager.Repository.Interfaces;

namespace OrgManager.Repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly OrgManagerContext _context;

        public OrganizationRepository(OrgManagerContext context)
        {
            _context = context;
        }

        public async Task<PageList<Organization>> GetAllOrganizationsAsync(PageParams pageParams)
        {
            IQueryable<Organization> query = _context.Organizations.
                Include(o => o.Phones).
                Include(o => o.Addresses);

            query = query.AsNoTracking()
                         .Where(o => o.Name.ToLower().Contains(pageParams.Term.ToLower()))
                         .OrderBy(o => o.Id);

            return await PageList<Organization>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Organization> GetOrganizationByIdAsync(int Id)
        {
            IQueryable<Organization> query = _context.Organizations.
                Include(o => o.Phones).
                Include(o => o.Addresses);

            query = query.AsNoTracking()
                         .Where(o => o.Id == Id)
                         .OrderBy(o => o.Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}