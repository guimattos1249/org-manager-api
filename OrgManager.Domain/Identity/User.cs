using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OrgManager.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public IEnumerable<UserRole>? UserRoles { get; set; }
        public string? CPF { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? EntryDate { get; set; }
        public int OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        public IEnumerable<Address>? Addresses { get; set; }
        public IEnumerable<Phone>? Phones { get; set; }
    }
}