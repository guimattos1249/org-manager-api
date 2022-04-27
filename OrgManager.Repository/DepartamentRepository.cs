using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrgManager.Domain;
using OrgManager.Domain.Identity;
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

        public async Task<Departament> GetDepartamentByIdAsync(int departamentId, int organizationId)
        {
            IQueryable<Departament> query = _context.Departaments.
                AsNoTracking().
                Where(d => 
                        d.Id == departamentId && 
                        d.OrganizationId == organizationId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Departament> GetAllUsersInDepartamentAsync(int departamentId, int organizationId)
        {
            IQueryable<Departament> query = _context.Departaments.
                Include(d => d.UserDepartament).
                ThenInclude(ud => ud.User);

            query = query.AsNoTracking().
                            Where(d => 
                        d.Id == departamentId && 
                        d.OrganizationId == organizationId);
                        
            return await query.FirstOrDefaultAsync();
        }

        public async Task<User> GetAllDepartamentsInUserAsync(int userId)
        {
            IQueryable<User> query = _context.Users.
                Include(u => u.UserDepartament)
                .ThenInclude(ud => ud.Departament);

            query = query.AsNoTracking().
                            Where(u => u.Id == userId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Departament[]> GetAllDepartamentsInOrganizationAsync(int organizationId)
        {
            IQueryable<Departament> query = _context.Departaments.
                AsNoTracking().
                Where(d => d.OrganizationId == organizationId);
            return await query.ToArrayAsync();
        }

        public async Task<Departament[]> GetAllDepartamentsWithAllUsersInOrganizationAsync(int organizationId)
        {
            IQueryable<Departament> query = _context.Departaments.
                Include(d => d.UserDepartament).
                ThenInclude(ud => ud.User).
                AsNoTracking().
                Where(d => d.OrganizationId == organizationId);
            return await query.ToArrayAsync();
        }
    }
}