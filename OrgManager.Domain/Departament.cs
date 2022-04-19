using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrgManager.Domain
{
    public class Departament
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public Guid OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        public IEnumerable<UserDepartament>? UserDepartament { get; set; }
    }
}