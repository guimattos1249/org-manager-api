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
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IAccountService _accountService;

        public OrganizationController(
            IOrganizationService organizationService,
            IAccountService accountService)
        {
            _organizationService = organizationService;
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), id) == null) 
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                var organization = await _organizationService.GetOrganizationByIdAsync(id);
                if(organization == null) return NoContent();

                return Ok(organization);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Organização. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrganizationDto model)
        {
            try
            {
                var organization = await _organizationService.AddOrganizations(model);
                if(organization == null) return NoContent();

                return Ok(organization);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar incluir Organização. Erro: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(OrganizationDto model)
        {
            try
            {
                UserUpdateDto user = await _accountService.UserExistsInOrganization(User.GetUserId(), model.Id);
                if( user == null ) 
                    return Unauthorized("Usuário não existe na Organização.");
                
                if( user.Function != "Leader" || user.Function != "Owner" )
                    return Unauthorized("Usuário não tem permissão para alterar a Organização");

                var organization = await _organizationService.UpdateOrganization(model);
                if(organization == null) return NoContent();

                return Ok(organization);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar alterar Organização. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                UserUpdateDto user = await _accountService.UserExistsInOrganization(User.GetUserId(), id);
                if( user == null ) 
                    return Unauthorized("Usuário não existe na Organização.");
                
                if( user.Function != "Owner" )
                    return Unauthorized("Usuário não tem permissão para excluir a Organização");

                if(await _organizationService.DeleteOrganization(id)) 
                    return Ok(new { message = "Deletado "}) ;
                else
                {
                    throw new Exception("Ocorreu um problema não específico ao tentar deletar o Evento");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar alterar Organização. Erro: {ex.Message}");
            }
        }
        
    }
}