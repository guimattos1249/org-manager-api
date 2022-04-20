using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrgManager.Domain;
using OrgManager.Repository.Contexts;
using OrgManager.Repository.Interfaces;

namespace OrgManager.Repository
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly OrgManagerContext _context;

        public PhoneRepository(OrgManagerContext context)
        {
            _context = context;
        }

        public async Task<Phone> GetPhoneUserByIdsAsync(int userId, int id)
        {
            IQueryable<Phone> query = _context.Phones;

            query = query.AsNoTracking()
                         .Where(ph => ph.UserId == userId &&
                                      ph.Id == id);


            return await query.FirstOrDefaultAsync();
        }

        public async Task<Phone> GetPhoneOrganizationByIdsAsync(int organizationId, int id)
        {
            IQueryable<Phone> query = _context.Phones;

            query = query.AsNoTracking()
                         .Where(ph => ph.OrganizationId == organizationId &&
                                      ph.Id == id);


            return await query.FirstOrDefaultAsync();
        }

        public async Task<Phone[]> GetAllByUserIdsAsync(int userId)
        {
            IQueryable<Phone> query = _context.Phones;

            query = query.AsNoTracking()
                         .Where(ph => ph.UserId == userId);


            return await query.ToArrayAsync();
        }

        public async Task<Phone[]> GetAllByOrganizationIdsAsync(int organizationId)
        {
            IQueryable<Phone> query = _context.Phones;

            query = query.AsNoTracking()
                         .Where(ph => ph.OrganizationId == organizationId);


            return await query.ToArrayAsync();
        }
    }
}