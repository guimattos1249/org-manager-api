using OrgManager.Domain.Identity;

namespace OrgManager.Domain
{
    public class Address
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public int Number { get; set; }
        public string? Distric { get; set; }
        public string? CEP { get; set; }
        public int? OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}