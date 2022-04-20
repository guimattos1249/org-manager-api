using System;
using OrgManager.Domain.Identity;

namespace OrgManager.Domain
{
    public class Phone
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public int? OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}