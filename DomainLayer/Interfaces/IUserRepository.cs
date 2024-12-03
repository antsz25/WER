using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> LoginUser(string email, string password);
        public Task<bool> RegisterUser(User user);
    }
}
