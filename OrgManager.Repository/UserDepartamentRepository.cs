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
    public class UserDepartamentRepository : GeneralRepository, IUserDepartamentRepository
    {
        private readonly OrgManagerContext _context;

        public UserDepartamentRepository(OrgManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserDepartament>  GetUserDepartamentByIdsAsync(int userId, int departamentId, int organizationId)
        {
            IQueryable<UserDepartament> query = _context.UserDepartaments.
                    Include(ud => ud.User).
                    Include(ud => ud.Departament);

            query = query.AsNoTracking()
                         .Where(ud => ud.DepartamentId == departamentId && 
                                ud.UserId == userId && 
                                ud.Departament.OrganizationId == organizationId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<UserDepartament> GetUserDepartamentByDepartamentIdAsync(int departamentId, int organizationId)
        {
            IQueryable<UserDepartament> query = _context.UserDepartaments.
                    Include(ud => ud.User).
                    Include(ud => ud.Departament);

            query = query.AsNoTracking()
                         .Where(ud => ud.DepartamentId == departamentId &&
                                ud.Departament.OrganizationId == organizationId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<UserDepartament> GetUserDepartamentByUserIdAsync(int userId)
        {
            IQueryable<UserDepartament> query = _context.UserDepartaments.
                    Include(ud => ud.User).
                    Include(ud => ud.Departament);

            query = query.AsNoTracking()
                         .Where(ud => ud.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }
    }
}