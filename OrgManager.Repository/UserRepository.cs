using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrgManager.Domain.Identity;
using OrgManager.Repository.Contexts;
using OrgManager.Repository.Interfaces;

namespace OrgManager.Repository
{
    public class UserRepository : GeneralRepository, IUserRespository
    {
        private readonly OrgManagerContext _context;

        public UserRepository(OrgManagerContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users
                                    .SingleOrDefaultAsync(user => user.UserName == userName.ToLower());
        }
    }
}