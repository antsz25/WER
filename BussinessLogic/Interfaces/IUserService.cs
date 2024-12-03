using BussinessLogic.ViewModels;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
    public interface IUserService
    {
        public Task<UserSessionVM> LoginUser(UserLoginVM userLogin);
        public Task<bool> RegisterUser(UserRegisterVM user);
    }
}
