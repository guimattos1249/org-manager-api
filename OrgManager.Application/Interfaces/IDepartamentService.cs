using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrgManager.Application.Dtos;
using OrgManager.Repository.Helpers;

namespace OrgManager.Application.Interfaces
{
    public interface IDepartamentService
    {
        Task<DepartamentDto> AddDepartament(int userId, DepartamentDto model);
        Task<bool> AddUserDepartament(int userId, int departamentId, UserDepartamentDto model);
        Task<DepartamentDto> UpdateDepartament(int userId, int organizationId, DepartamentDto model);
        Task<bool> DeleteDepartament(int organizationId, int departamentId);
        Task<bool> DeleteUserDepartament(int organizationId, int departamentId);

        Task<DepartamentDto> GetDepartamentByIdAsync(int departamentId, int organizationId);
        Task<DepartamentDto> GetAllUsersInDepartamentAsync(int departamentId, int organizationId);
        Task<UserUpdateDto> GetAllDepartamentsInUserAsync(int userId);
        Task<DepartamentDto[]> GetAllDepartamentsInOrganizationAsync(int organizationId);
        Task<DepartamentDto[]> GetAllDepartamentsWithAllUsersInOrganizationAsync(int organizationId);
    }
}