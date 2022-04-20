using System.ComponentModel.DataAnnotations;

namespace OrgManager.Application.Dtos
{
    public class PhoneDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Number { get; set; }
        public int? OrganizationId { get; set; }
        public OrganizationDto? Organization { get; set; }
        public int? UserId { get; set; }
        public UserDto? User { get; set; }
    }
}