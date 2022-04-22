using Microsoft.EntityFrameworkCore;
using OrgManager.Domain;
using OrgManager.Repository.Contexts;
using OrgManager.Repository.Helpers;
using OrgManager.Repository.Interfaces;

namespace OrgManager.Repository
{
    public class OrganizationRepository : GeneralRepository, IOrganizationRepository
    {
        private readonly OrgManagerContext _context;

        public OrganizationRepository(OrgManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Organization> GetOrganizationByIdAsync(int Id)
        {
            IQueryable<Organization> query = _context.Organizations.
                Include(o => o.Phones).
                Include(o => o.Addresses);

            query = query.AsNoTracking()
                         .OrderBy(o => o.Id)
                         .Where(o => o.Id == Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
