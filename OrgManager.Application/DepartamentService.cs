using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OrgManager.Application.Dtos;
using OrgManager.Application.Interfaces;
using OrgManager.Domain;
using OrgManager.Domain.Enum;
using OrgManager.Domain.Identity;
using OrgManager.Repository.Helpers;
using OrgManager.Repository.Interfaces;
using src.OrgManager.Application.Interfaces;

namespace src.OrgManager.Application
{
    public class DepartamentService : IDepartamentService
    {
        private readonly IDepartamentRepository _departamentRepository;
        private readonly IUserDepartamentRepository _userDepartamentRepository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public DepartamentService(
            IDepartamentRepository departamentRepository,
            IUserDepartamentRepository userDepartamentRepository,
            IAccountService accountService,
            IMapper mapper
        )
        {
            _departamentRepository = departamentRepository;
            _userDepartamentRepository = userDepartamentRepository;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<DepartamentDto> AddDepartament(int userId, DepartamentDto model)
        {
            try
            {
                Departament departament = _mapper.Map<Departament>(model);
                _departamentRepository.Add<Departament>(departament);
                if(await _departamentRepository.SaveChangesAsync())
                {
                    Departament departamentReturn = await _departamentRepository.GetDepartamentByIdAsync(departament.Id, departament.OrganizationId);
                    
                    UserDepartament userDepartament = new UserDepartament();

                    userDepartament.DepartamentId = departamentReturn.Id;
                    userDepartament.UserId = userId;
                    userDepartament.Function = Function.Leader;


                    _userDepartamentRepository.Add<UserDepartament>(userDepartament);

                    if(await _departamentRepository.SaveChangesAsync())
                        return _mapper.Map<DepartamentDto>(departamentReturn);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public async Task<bool> AddUserDepartament(int userId, int departamentId, UserDepartamentDto model)
        {
            try
            {
                UserUpdateDto user = await _accountService.GetUserByIdAsync(userId);
                UserDepartament userDepartamentExists = await _userDepartamentRepository.GetUserDepartamentByIdsAsync(userId, departamentId, user.OrganizationId);
                if(userDepartamentExists != null) return false;
                UserDepartament userDepartament = _mapper.Map<UserDepartament>(model);
                DepartamentDto departament = await GetDepartamentByIdAsync(departamentId, user.OrganizationId);
                _userDepartamentRepository.Add<UserDepartament>(userDepartament);
                if(await _userDepartamentRepository.SaveChangesAsync())
                {
                    UserDepartament userDepartamentReturn = 
                    await _userDepartamentRepository.GetUserDepartamentByIdsAsync(userId, departamentId, user.OrganizationId);

                    return userDepartament == null ? false : true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DepartamentDto> UpdateDepartament(int userId, int organizationId, DepartamentDto model)
        {
            try
            {
                Departament departament = await _departamentRepository.GetDepartamentByIdAsync(model.Id, organizationId);
                if(departament == null) return null;
                _departamentRepository.Update<Departament>(_mapper.Map<Departament>(model));
                if(await _departamentRepository.SaveChangesAsync())
                {
                    Departament departamentReturn = await _departamentRepository.GetDepartamentByIdAsync(model.Id, organizationId);

                    return _mapper.Map<DepartamentDto>(departamentReturn);
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserDepartamentDto> UpdateUserDepartament(int userId, int organizationId, UserDepartamentDto model)
        {
            try
            {
                UserDepartament userDepartament = await _userDepartamentRepository.GetUserDepartamentByIdsAsync(userId, model.DepartamentId, organizationId);
                model.UserId = userId;
                if(userDepartament == null) return null;
                if(userDepartament.Function == Function.Leader || userDepartament.Function == Function.Owner)
                {
                    _userDepartamentRepository.Update<UserDepartament>(_mapper.Map<UserDepartament>(model));
                    if(await _userDepartamentRepository.SaveChangesAsync())
                    {
                        UserDepartament userDepartamentReturn = await _userDepartamentRepository.GetUserDepartamentByIdsAsync(userId, model.DepartamentId, organizationId); 

                        return _mapper.Map<UserDepartamentDto>(userDepartamentReturn);
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteDepartament(int organizationId, int departamentId)
        {
             try
            {            
                Departament departamentToDelete = await _departamentRepository.GetDepartamentByIdAsync(departamentId, organizationId);
                if(departamentToDelete == null) return false;
                _departamentRepository.Delete<Departament>(departamentToDelete);
                return await _departamentRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteUserDepartament(int organizationId, int departamentId)
        {
             try
            {            
                UserDepartament userDepartamentToDelete = await _userDepartamentRepository.GetUserDepartamentByDepartamentIdAsync(departamentId, organizationId);
                if(userDepartamentToDelete == null) return false;
                _userDepartamentRepository.Delete<UserDepartament>(userDepartamentToDelete);
                return await _userDepartamentRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DepartamentDto> GetDepartamentByIdAsync(int departamentId, int organizationId)
        {
            try
            {
                Departament departament = await _departamentRepository.GetDepartamentByIdAsync(departamentId, organizationId);
                if(departament == null) return null;
                return _mapper.Map<DepartamentDto>(departament);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DepartamentDto> GetAllUsersInDepartamentAsync(int departamentId, int organizationId)
        {
            try
            {
                Departament departament = await _departamentRepository.GetAllUsersInDepartamentAsync(departamentId, organizationId);
                if(departament == null) return null;
                return _mapper.Map<DepartamentDto>(departament);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserUpdateDto> GetAllDepartamentsInUserAsync(int userId)
        {
            try
            {
                User user = await _departamentRepository.GetAllDepartamentsInUserAsync(userId);
                if(user == null) return null;
                return _mapper.Map<UserUpdateDto>(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DepartamentDto[]> GetAllDepartamentsInOrganizationAsync(int organizationId)
        {
            Departament[] departaments = await _departamentRepository.GetAllDepartamentsInOrganizationAsync(organizationId);
            if(departaments == null) return null;
            return _mapper.Map<DepartamentDto[]>(departaments);
        }

        public async Task<DepartamentDto[]> GetAllDepartamentsWithAllUsersInOrganizationAsync(int organizationId)
        {
            Departament[] departaments = await _departamentRepository.GetAllDepartamentsWithAllUsersInOrganizationAsync(organizationId);
            if(departaments == null) return null;
            return _mapper.Map<DepartamentDto[]>(departaments);
        }
    }
}