using System.ComponentModel.DataAnnotations;

namespace OrgManager.Application.Dtos
{
    public class OrganizationDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório."),
        StringLength(50, MinimumLength = 4, ErrorMessage = "O campo {0} deve ter entre 4 a 100 caracteres")]
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? CNPJ { get; set; }
        public string? OpeningDate { get; set; }
        public IEnumerable<AddressDto>? Addresses { get; set; }
        public IEnumerable<PhoneDto>? Phones { get; set; }
    }
}