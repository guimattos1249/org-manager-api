using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrgManager.Application.Dtos;
using OrgManager.Application.Interfaces;
using OrgManager.Domain.Identity;
using OrgManager.Repository.Interfaces;

namespace OrgManager.Application
{
    public class AccountService : IAccountService
    {
         private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AccountService(UserManager<User> userManager,
                                SignInManager<User> signInManager,
                                IMapper mapper,
                                IUserRepository userRepository)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string Password)
        {
            try
            {
                var user = await _userManager.Users
                                                .SingleOrDefaultAsync(user => user.UserName == userUpdateDto.UserName.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(user, Password, false);
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Erro ao tentar verificar senha. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> CreatAccountAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);
                
                if(result.Succeeded)
                {
                    var userToReturn = _mapper.Map<UserUpdateDto>(user);
                    return userToReturn;
                }

                return null;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Erro ao tentar Criar Conta. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByUserNameAsync(string userName)
        {
            try
            {
                var user = await _userRepository.GetUserByUserNameAsync(userName);
                if(user == null) return null;

                var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
                return userUpdateDto;

            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Erro ao tentar buscar usuário por username. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _userRepository.GetUserByUserNameAsync(userUpdateDto.UserName);
                if(user == null) return null;

                userUpdateDto.Id = user.Id;

                _mapper.Map(userUpdateDto, user);

                if(userUpdateDto.Password != null) {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);
                }


                _userRepository.Update<User>(user);

                if(await _userRepository.SaveChangesAsync())
                {
                    var userRetorno = await _userRepository.GetUserByUserNameAsync(user.UserName);

                    return _mapper.Map<UserUpdateDto>(userRetorno);
                }

                return null;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            try
            {
                return await _userManager.Users
                                            .AnyAsync(user => user.UserName == userName.ToLower());
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Erro ao verificar se o usuário existe. Erro: {ex.Message}");
            }
        }
    }
}