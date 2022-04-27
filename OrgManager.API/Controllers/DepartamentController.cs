using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrgManager.Application.Dtos;
using OrgManager.Application.Interfaces;
using src.OrgManager.API.Extensions;

namespace OrgManager.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DepartamentController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IDepartamentService _departamentService;

        public DepartamentController(
            IAccountService accountService,
            IDepartamentService departamentService
        )
        {
            _accountService = accountService;
            _departamentService = departamentService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DepartamentDto model)
        {
            try
            {
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), model.OrganizationId) == null)
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                DepartamentDto departament = await _departamentService.AddDepartament(User.GetUserId(), model);
                if(departament == null) return NoContent();

                return Ok(departament);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar incluir Departamento. Erro: {ex.Message}");
            }
        }

        [HttpPost("bond/{organizationId}")]
        public async Task<IActionResult> PostUserDepartament(int organizationId, UserDepartamentDto model)
        {
            try
            {
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null)
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                bool userDepartament = await _departamentService.AddUserDepartament(User.GetUserId(), model.DepartamentId, model);
                if(userDepartament == false) return NoContent();

                return Ok(userDepartament);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar incluir Vinculo do usuário com o Departamento. Erro: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(DepartamentDto model)
        {
            try
            {
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), model.OrganizationId) == null)
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                DepartamentDto departament = await _departamentService.UpdateDepartament(User.GetUserId(), model.OrganizationId, model);
                if(departament == null) return NoContent();

                return Ok(departament);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Alterar Departamento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{organizationId}/{departamentId}")]
        public async Task<IActionResult> Delete(int organizationId, int departamentId )
        {
            try
            {
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null)
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                bool departament = await _departamentService.DeleteDepartament(organizationId, departamentId);
                if(departament == false) return this.StatusCode(StatusCodes.Status404NotFound, $"Departamento não encontrado para exclusão.");

                return Ok(departament);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Excluir Departamento. Erro: {ex.Message}");
            }
        }

        [HttpGet("{departamentId}/{organizationId}")]
        public async Task<IActionResult> GetDepartamentByIdAsync(int departamentId, int organizationId)
        {
            try
            {
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null)
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                DepartamentDto departament = await _departamentService.GetDepartamentByIdAsync(departamentId, organizationId);

                if(departament == null) return NoContent();

                return Ok(departament);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar consultar Departamento. Erro: {ex.Message}");
            }
        }

        [HttpGet("User/{departamentId}/{organizationId}")]
        public async Task<IActionResult> GetAllUsersInDepartamentAsync(int departamentId, int organizationId)
        {
            try
            {
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null)
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                DepartamentDto departament = await _departamentService.GetAllUsersInDepartamentAsync(departamentId, organizationId);
                if(departament == null) return NoContent();

                return Ok(departament);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar consultar Departamento. Erro: {ex.Message}");
            }
        }

        [HttpGet("User/{organizationId}")]
        public async Task<IActionResult> GetAllDepartamentsInUserAsync(int organizationId)
        {
            try
            {
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null)
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                UserUpdateDto user = await _departamentService.GetAllDepartamentsInUserAsync(User.GetUserId());

                if(user == null) return NoContent();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar consultar Departamento. Erro: {ex.Message}");
            }
        }

        [HttpGet("Organization/{organizationId}")]
        public async Task<IActionResult> GetAllDepartamentsInOrganizationAsync(int organizationId)
        {
            try
            {
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null)
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                DepartamentDto[] departaments = await _departamentService.GetAllDepartamentsInOrganizationAsync(organizationId);

                if(departaments == null) return NoContent();

                return Ok(departaments);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar consultar Departamento. Erro: {ex.Message}");
            }
        }
        
        [HttpGet("{organizationId}")]
        public async Task<IActionResult> GetAllDepartamentsWithAllUsersInOrganizationAsync(int organizationId)
        {
            try
            {
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null)
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                DepartamentDto[] departaments = await _departamentService.GetAllDepartamentsWithAllUsersInOrganizationAsync(organizationId);

                if(departaments == null) return NoContent();

                return Ok(departaments);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar consultar Departamento. Erro: {ex.Message}");
            }
        }
    }
}