using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OrgManager.Application.Dtos;
using OrgManager.Domain;
using OrgManager.Domain.Identity;

namespace OrgManager.Application.Helpers
{
    public class OrgManagerProfile : Profile
    {
        public OrgManagerProfile()
        {
            CreateMap<Organization, OrganizationDto>().ReverseMap();
            CreateMap<OrganizationDto, Organization>().ReverseMap();
            CreateMap<Departament, DepartamentDto>().ReverseMap();
            CreateMap<UserDepartament, UserDepartamentDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Phone, PhoneDto>().ReverseMap();
            
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
        }
    }
}