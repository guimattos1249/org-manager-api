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
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public AddressController(
            IAddressService addressService,
            IMapper mapper,
            IAccountService accountService
        )
        {
            _addressService = addressService;
            _mapper = mapper;
            _accountService = accountService;
        }

        [HttpPost("User")]
        public async Task<IActionResult> PostByUser(AddressDto model)
        {
            try
            {
                var organizationId = 0;
                int userId = User.GetUserId();
                var address = await _addressService.AddAddress(userId, organizationId, model);
                if(address == null) return NoContent();

                return Ok(address);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar incluir Endereço. Erro: {ex.Message}");
            }
        }

        [HttpPost("Organization/{organizationId}")]
        public async Task<IActionResult> PostByOrganization(int organizationId, AddressDto model)
        {
            try
            {
                var userId = 0;
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null) 
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                var address = await _addressService.AddAddress(userId, organizationId, model);
                if(address == null) return NoContent();

                return Ok(address);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar incluir Telefone. Erro: {ex.Message}");
            }
        }

        [HttpPut("User")]
        public async Task<IActionResult> PutByUser(AddressDto model)
        {
            try
            {
                int organizationId = 0;
                int userId = User.GetUserId();
                AddressDto address = await _addressService.UpdateAddress(userId, organizationId, model);
                if(address == null) return NoContent();

                return Ok(address);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Alterar Telefone. Erro: {ex.Message}");
            }
        }

        [HttpGet("User/{addressId}")]
        public async Task<IActionResult> GetAddress(int addressId)
        {
            try
            {
                int organizationId = 0;
                int userId = User.GetUserId();
                AddressDto address = await _addressService.GetAddressByIdAsync(userId, organizationId, addressId);

                if(address == null) return NoContent();

                return Ok(address);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar consultar Telefone. Erro: {ex.Message}");
            }
        }

        [HttpGet("Organization/{organizationId}/{addressId}")]
        public async Task<IActionResult> GetAddress(int organizationId, int addressId)
        {
            try
            {
                int userId = 0;
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null) 
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                AddressDto address = await _addressService.GetAddressByIdAsync(userId, organizationId, addressId);

                if(address == null) return NoContent();

                return Ok(address);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar consultar Telefone. Erro: {ex.Message}");
            }
        }

        [HttpGet("User")]
        public async Task<IActionResult> GetAllAddresses()
        {
            try
            {
                int organizationId = 0;
                int userId = User.GetUserId();
                AddressDto[] addresss = await _addressService.GetAllAddressesByIdAsync(userId, organizationId);

                if(addresss == null) return NoContent();

                return Ok(addresss);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar consultar Telefone. Erro: {ex.Message}");
            }
        }

        [HttpGet("Organization/{organizationId}")]
        public async Task<IActionResult> GetAllAddresses(int organizationId)
        {
            try
            {
                int userId = 0;
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null) 
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                AddressDto[] addresss = await _addressService.GetAllAddressesByIdAsync(userId, organizationId);

                if(addresss == null) return NoContent();

                return Ok(addresss);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar consultar Telefone. Erro: {ex.Message}");
            }
        }

        [HttpPut("Organization/{organizationId}")]
        public async Task<IActionResult> PutByOrganization(int organizationId, AddressDto model)
        {
            try
            {
                int userId = 0;
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null) 
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                AddressDto address = await _addressService.UpdateAddress(userId, organizationId, model);
                if(address == null) return NoContent();

                return Ok(address);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Alterar Telefone. Erro: {ex.Message}");
            }
        }

        [HttpDelete("User/{addressId}")]
        public async Task<IActionResult> DeleteByUser(int addressId)
        {
            try
            {
                int organizationId = 0;
                int userId = User.GetUserId();
                bool address = await _addressService.DeleteAddress(userId, organizationId, addressId);
                if(address == false) return this.StatusCode(StatusCodes.Status404NotFound, $"Telefone não encontrado para exclusão.");

                return Ok(address);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Excluir Telefone. Erro: {ex.Message}");
            }
        }

        [HttpDelete("Organization/{organizationId}/{addressId}")]
        public async Task<IActionResult> DeleteByOrganization(int organizationId, int addressId)
        {
            try
            {
                int userId = 0;
                if(await _accountService.UserExistsInOrganization(User.GetUserId(), organizationId) == null) 
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Usuário não existe na Organização.");
                bool address = await _addressService.DeleteAddress(userId, organizationId, addressId);
                if(address == false) return this.StatusCode(StatusCodes.Status404NotFound, $"Telefone não encontrado para exclusão.");

                return Ok(address);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Excluir Telefone. Erro: {ex.Message}");
            }
        }
    }
}