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
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar incluir Telefone. Erro: {ex.Message}");
            }
        }

        [HttpPost("Organization/{organizationId}")]
        public async Task<IActionResult> PostByOrganization(int organizationId, PhoneDto model)
        {
            try
            {
                var userId = 0;
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null) 
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                var phone = await _phoneService.AddPhone(userId, organizationId, model);
                if(phone == null) return NoContent();

                return Ok(phone);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar incluir Telefone. Erro: {ex.Message}");
            }
        }

        [HttpPut("User")]
        public async Task<IActionResult> PutByUser(PhoneDto model)
        {
            try
            {
                int organizationId = 0;
                int userId = User.GetUserId();
                PhoneDto phone = await _phoneService.UpdatePhone(userId, organizationId, model);
                if(phone == null) return NoContent();

                return Ok(phone);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Alterar Telefone. Erro: {ex.Message}");
            }
        }

        [HttpGet("User/{phoneId}")]
        public async Task<IActionResult> GetPhone(int phoneId)
        {
            try
            {
                int organizationId = 0;
                int userId = User.GetUserId();
                PhoneDto phone = await _phoneService.GetPhoneByIdAsync(userId, organizationId, phoneId);

                if(phone == null) return NoContent();

                return Ok(phone);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar consultar Telefone. Erro: {ex.Message}");
            }
        }

        [HttpGet("Organization/{organizationId}/{phoneId}")]
        public async Task<IActionResult> GetPhone(int organizationId, int phoneId)
        {
            try
            {
                int userId = 0;
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null) 
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                PhoneDto phone = await _phoneService.GetPhoneByIdAsync(userId, organizationId, phoneId);

                if(phone == null) return NoContent();

                return Ok(phone);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar consultar Telefone. Erro: {ex.Message}");
            }
        }

        [HttpGet("User")]
        public async Task<IActionResult> GetAllPhones()
        {
            try
            {
                int organizationId = 0;
                int userId = User.GetUserId();
                PhoneDto[] phones = await _phoneService.GetAllPhonesByIdAsync(userId, organizationId);

                if(phones == null) return NoContent();

                return Ok(phones);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar consultar Telefone. Erro: {ex.Message}");
            }
        }

        [HttpGet("Organization/{organizationId}")]
        public async Task<IActionResult> GetAllPhones(int organizationId)
        {
            try
            {
                int userId = 0;
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null) 
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                PhoneDto[] phones = await _phoneService.GetAllPhonesByIdAsync(userId, organizationId);

                if(phones == null) return NoContent();

                return Ok(phones);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar consultar Telefone. Erro: {ex.Message}");
            }
        }

        [HttpPut("Organization/{organizationId}")]
        public async Task<IActionResult> PutByOrganization(int organizationId, PhoneDto model)
        {
            try
            {
                int userId = 0;
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null) 
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                PhoneDto phone = await _phoneService.UpdatePhone(userId, organizationId, model);
                if(phone == null) return NoContent();

                return Ok(phone);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Alterar Telefone. Erro: {ex.Message}");
            }
        }

        [HttpDelete("User/{phoneId}")]
        public async Task<IActionResult> DeleteByUser(int phoneId)
        {
            try
            {
                int organizationId = 0;
                int userId = User.GetUserId();
                bool phone = await _phoneService.DeletePhone(userId, organizationId, phoneId);
                if(phone == false) return this.StatusCode(StatusCodes.Status404NotFound, $"Telefone não encontrado para exclusão.");

                return Ok(phone);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Excluir Telefone. Erro: {ex.Message}");
            }
        }

        [HttpDelete("Organization/{organizationId}/{phoneId}")]
        public async Task<IActionResult> DeleteByOrganization(int organizationId, int phoneId)
        {
            try
            {
                int userId = 0;
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null) 
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                bool phone = await _phoneService.DeletePhone(userId, organizationId, phoneId);
                if(phone == false) return this.StatusCode(StatusCodes.Status404NotFound, $"Telefone não encontrado para exclusão.");

                return Ok(phone);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Excluir Telefone. Erro: {ex.Message}");
            }
        }
    }
}