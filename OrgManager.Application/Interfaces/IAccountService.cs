using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OrgManager.Application.Dtos;

namespace OrgManager.Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> UserExists(string userName);
        Task<UserUpdateDto> GetUserByUserNameAsync(string userName);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string Password);
        Task<UserUpdateDto> CreatAccountAsync(UserDto userDto);
        Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);
    }
}