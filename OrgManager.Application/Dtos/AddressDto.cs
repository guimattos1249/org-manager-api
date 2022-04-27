using System.ComponentModel.DataAnnotations;

namespace OrgManager.Application.Dtos
{
    public class AddressDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int Number { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Distric { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? City { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? State { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? CEP { get; set; }
        public int? OrganizationId { get; set; }
        public OrganizationDto? Organization { get; set; }
        public int? UserId { get; set; }
        public UserDto? User { get; set; }
    }
}