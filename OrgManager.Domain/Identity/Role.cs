using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OrgManager.Domain.Identity
{
    public class Role : IdentityRole<int>
    {
        public IEnumerable<UserRole>? UserRoles { get; set; }
    }
}