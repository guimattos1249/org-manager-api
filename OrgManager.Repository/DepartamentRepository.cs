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

        public async Task<Departament> GetAllUsersInDepartamentByIdsAsync(int departamentId)
        {
            IQueryable<Departament> query = _context.Departaments.
                    Include(d => d.UserDepartament).
                    ThenInclude(ud => ud.User);


            query = query.AsNoTracking()
                         .Where(dp => dp.Id == departamentId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<User> GetAllDepartamentsInUserByIdsAsync(int userId)
        {
            IQueryable<User> query = _context.Users.
                    Include(u => u.UserDepartament).
                    ThenInclude(ud => ud.Departament);


            query = query.AsNoTracking()
                         .Where(dp => dp.Id == userId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<UserDepartament> GetUserDepartamentByIdsAsync(int departamentId, int userId)
        {
            IQueryable<UserDepartament> query = _context.UserDepartaments.
                    Include(ud => ud.User).
                    Include(ud => ud.Departament);

            query = query.AsNoTracking()
                         .Where(ud => ud.DepartamentId == departamentId && 
                                ud.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }
    }
}