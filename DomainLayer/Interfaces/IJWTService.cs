using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces
{
    public interface IJWTService
    {
        string JWTKey { get; }
        public string GenerateJWT(User user);
        public string encryptSHA256(string text);
    }
}
