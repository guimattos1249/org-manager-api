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

        public async Task<Address> GetAddressByIdsAsync(int organizationId, int addressId, int userId = 0)
        {
            IQueryable<Address> query = _context.Addresses;

            if(userId != 0)
            {
                query = query.AsNoTracking()
                            .Where(ph => ph.UserId == userId &&
                                        ph.Id == addressId);
            }
            else
                query = query.AsNoTracking()
                            .Where(ph => ph.OrganizationId == organizationId &&
                                        ph.Id == addressId);


            return await query.FirstOrDefaultAsync();
        }

        public async Task<Address[]> GetAllByAddressesIdAsync(int userId, int organizationId)
        {
            IQueryable<Address> query = _context.Addresses;

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