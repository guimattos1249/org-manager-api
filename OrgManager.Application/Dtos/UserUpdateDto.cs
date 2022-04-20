using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrgManager.Application.Dtos
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? CPF { get; set; }
        public string? BirthDate { get; set; }
        public string? EntryDate { get; set; }
        public string? Function { get; set; }
        public int OrganizationId { get; set; }
        public OrganizationDto? Organization { get; set; }
        public string? UserName { get; set; }
        public string? Descricao { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public IEnumerable<AddressDto>? Addresses { get; set; }
        public IEnumerable<PhoneDto>? Phones { get; set; }
        public IEnumerable<DepartamentDto>? Departaments { get; set; }
    }
}