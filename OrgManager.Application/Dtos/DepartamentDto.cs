namespace OrgManager.Application.Dtos
{
    public class DepartamentDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public int OrganizationId { get; set; }
        public OrganizationDto? Organization { get; set; }
        public IEnumerable<UserDepartamentDto>? UserDepartament { get; set; }
    }
}