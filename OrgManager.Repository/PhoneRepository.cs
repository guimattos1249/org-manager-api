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
    public class PhoneRepository : GeneralRepository, IPhoneRepository
    {
        private readonly OrgManagerContext _context;

        public PhoneRepository(OrgManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Phone> GetPhoneByIdAsync(int userId, int organizationId, int phoneId)
        {
            IQueryable<Phone> query = _context.Phones;

            if(userId != 0)
            {
                query = query.AsNoTracking()
                            .Where(ph => ph.UserId == userId &&
                                        ph.Id == phoneId);
            }
            else
                query = query.AsNoTracking()
                            .Where(ph => ph.OrganizationId == organizationId &&
                                        ph.Id == phoneId);


            return await query.FirstOrDefaultAsync();
        }

        public async Task<Phone[]> GetAllPhonesByIdAsync(int userId, int organizationId)
        {
            IQueryable<Phone> query = _context.Phones;

            if(userId != 0)
            {
                query = query.AsNoTracking()
                            .Where(ph => ph.UserId == userId);
            }
            else
            {
                query = query.AsNoTracking()
                            .Where(ph => ph.OrganizationId == organizationId);
            }


            return await query.ToArrayAsync();
        }
    }
}