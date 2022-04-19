using System;
using OrgManager.Domain.Enum;
using OrgManager.Domain.Identity;

namespace OrgManager.Domain
{
    public class UserDepartament
    {
        public int DepartamentId { get; set; }
        public Departament? Departament { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public DepartamentFunction Function { get; set; }
    }
}