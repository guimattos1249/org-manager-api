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
    public class AddressRepository : GeneralRepository, IAddressRepository
    {
        private readonly OrgManagerContext _context;

        public AddressRepository(OrgManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Address> GetAddressUserByIdsAsync(int userId, int id)
        {
            IQueryable<Address> query = _context.Addresses;

            query = query.AsNoTracking()
                         .Where(ad => ad.UserId == userId &&
                                      ad.Id == id);


            return await query.FirstOrDefaultAsync();
        }

        public async Task<Address> GetAddressOrganizationByIdsAsync(int organizationId, int id)
        {
            IQueryable<Address> query = _context.Addresses;

            query = query.AsNoTracking()
                         .Where(ad => ad.OrganizationId == organizationId &&
                                      ad.Id == id);


            return await query.FirstOrDefaultAsync();
        }

        public async Task<Address[]> GetAllByUserIdsAsync(int userId)
        {
            IQueryable<Address> query = _context.Addresses;

            query = query.AsNoTracking()
                         .Where(ad => ad.UserId == userId);


            return await query.ToArrayAsync();
        }

        public async Task<Address[]> GetAllByOrganizationIdsAsync(int organizationId)
        {
            IQueryable<Address> query = _context.Addresses;

            query = query.AsNoTracking()
                         .Where(ad => ad.OrganizationId == organizationId);


            return await query.ToArrayAsync();
        }
    }
}