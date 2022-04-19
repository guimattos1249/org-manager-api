using System;

namespace OrgManager.Domain
{
    public class Address
    {
        public Guid Id { get; set; }
        public string? Street { get; set; }
        public int Number { get; set; }
        public string? Distric { get; set; }
        public string? CEP { get; set; }
    }
}