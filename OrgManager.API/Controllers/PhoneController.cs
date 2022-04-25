using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrgManager.Application.Dtos;
using OrgManager.Application.Interfaces;
using src.OrgManager.API.Extensions;
using src.OrgManager.Application.Interfaces;

namespace src.OrgManager.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PhoneController : Controller
    {
        private readonly IPhoneService _phoneService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public PhoneController(
            IPhoneService phoneService,
            IMapper mapper,
            IAccountService accountService
        )
        {
            _phoneService = phoneService;
            _mapper = mapper;
            _accountService = accountService;
        }

        [HttpPost("User")]
        public async Task<IActionResult> PostByUser(PhoneDto model)
        {
            try
            {
                var organizationId = 0;
                int userId = User.GetUserId();
                var phone = await _phoneService.AddPhone(userId, organizationId, model);
                if(phone == null) return NoContent();

                return Ok(phone);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar incluir Organização. Erro: {ex.Message}");
            }
        }

        [HttpPost("Organization/{id}")]
        public async Task<IActionResult> PostByOrganization(int id, PhoneDto model)
        {
            try
            {
                var userId = 0;
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), id) == null) 
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                var phone = await _phoneService.AddPhone(userId, id, model);
                if(phone == null) return NoContent();

                return Ok(phone);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar incluir Organização. Erro: {ex.Message}");
            }
        }

        [HttpDelete("Organization/{organizationId}/{phoneId}")]
        public async Task<IActionResult> DeleteByOrganization(int organizationId, int phoneId)
        {
            try
            {
                var userId = 0;
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null) 
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                var phone = await _phoneService.DeletePhone(userId, organizationId, phoneId);
                if(phone == false) return this.StatusCode(StatusCodes.Status404NotFound, $"Telefone não encontrado para exclusão.");

                return Ok(phone);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar incluir Organização. Erro: {ex.Message}");
            }
        }

        [HttpDelete("User/{phoneId}")]
        public async Task<IActionResult> DeleteByUser(int phoneId)
        {
            try
            {
                var organizationId = 0;
                int userId = User.GetUserId();
                var phone = await _phoneService.DeletePhone(userId, organizationId, phoneId);
                if(phone == false) return this.StatusCode(StatusCodes.Status404NotFound, $"Telefone não encontrado para exclusão.");

                return Ok(phone);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar incluir Organização. Erro: {ex.Message}");
            }
        }
    }
}