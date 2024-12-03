using BussinessLogic.Interfaces;
using BussinessLogic.Mappers;
using BussinessLogic.ViewModels;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserSessionVM> LoginUser(UserLoginVM userLogin)
        {
            User? userEntity = await _userRepository.LoginUser(userLogin.Email, userLogin.Password);
            UserSessionVM session = null;
            if (userEntity != null)
            {
                session = userEntity.ToViewSession();
            }
            return session;
        }

        public async Task<bool> RegisterUser(UserRegisterVM user)
        {
            User userEntity = user.ToRegisterEntity();
            return await _userRepository.RegisterUser(userEntity);
        }
    }
}
