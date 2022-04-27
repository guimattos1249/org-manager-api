namespace OrgManager.Application.Dtos
{
    public class UserDepartamentDto
    {
        public int DepartamentId { get; set; }
        public DepartamentDto? Departament { get; set; }
        public int UserId { get; set; }
        public UserDto? User { get; set; }
        public string? Function { get; set; }
    }
}