using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrgManager.Domain
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? CNPJ { get; set; }
        public DateTime? OpeningDate { get; set; }
        public IEnumerable<Address>? Addresses { get; set; }
        public IEnumerable<Phone>? Phones { get; set; }
    }
}