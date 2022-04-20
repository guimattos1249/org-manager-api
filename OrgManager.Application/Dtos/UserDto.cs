using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrgManager.Application.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório."),
        StringLength(50, MinimumLength = 4, ErrorMessage = "O campo {0} deve ter entre 4 a 50 caracteres")]
        public string? UserName { get; set; }

        [Display(Name = "e-mail"),
        Required(ErrorMessage = "O campo {0} é obrigatório."),
        EmailAddress(ErrorMessage = "O campo {0} precisa ser válido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório."),
        StringLength(50, MinimumLength = 4, ErrorMessage = "O campo {0} deve ter entre 4 a 50 caracteres")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório."),
        StringLength(50, ErrorMessage = "O campo {0} deve ter no máximo 50 caracteres")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório."),
        StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo 100 caracteres")]
        public string? LastName { get; set; }
    }
}