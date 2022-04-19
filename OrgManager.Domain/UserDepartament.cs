using System;
using OrgManager.Domain.Enum;
using ProEventos.Domain.Identity;

namespace OrgManager.Domain
{
    public class UserDepartament
    {
        public Guid DepartamentId { get; set; }
        public Departament? Departament { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public DepartamentFunction Function { get; set; }
    }
}