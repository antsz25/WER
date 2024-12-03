using Arch.EntityFrameworkCore.UnitOfWork;
using DataLayer.Context;
using DataLayer.Utils;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class UserRepository : Repository<User>, IRepository<User>, IUserRepository
    {
        private readonly IJWTService _JWTService;
        public UserRepository(MainContext dbContext, IJWTService JWTService) : base(dbContext)
        {
            _JWTService = JWTService;
        }

        public async Task<User> LoginUser(string email, string password)
        {
            try
            {
                User? user = await _dbContext.Set<User>()
                    .Include(x => x.Works)
                    .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && x.Password == _JWTService.encryptSHA256(password));
                if (user == null)
                {
                    return null;
                }
                user.Token = _JWTService.GenerateJWT(user);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RegisterUser(User user)
        {
            try
            {
                User? existsUser = await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Email.ToLower() == user.Email.ToLower());
                if (existsUser != null)
                {
                    return false;
                }
                using (HttpClient client = new HttpClient())
                {
                    string baseUrl = "https://picsum.photos/200";
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        user.Photo = res.RequestMessage.RequestUri.ToString();
                    }
                }
                user.Password = _JWTService.encryptSHA256(user.Password);
                await _dbContext.Set<User>().AddAsync(user);
                int result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
