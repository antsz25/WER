using Arch.EntityFrameworkCore.UnitOfWork;
using DataLayer.Context;
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
    public class EnterpriseRepository : Repository<Enterprise>, IRepository<Enterprise>, IEnterpriseRepository
    {
        public EnterpriseRepository(MainContext dbContext) : base(dbContext) { }
        public async Task<List<Enterprise>> GetEnterprises()
        {
            return await _dbContext.Set<Enterprise>().ToListAsync();
        }

        public async Task<bool> SaveEnterprise(Enterprise enterprise)
        {
            try
            {
                Enterprise? exists = await _dbContext.Set<Enterprise>().FirstOrDefaultAsync(x => x.Name.ToLower() == enterprise.Name.ToLower());
                if (exists == null)
                {
                    await _dbContext.Set<Enterprise>().AddAsync(enterprise);
                    int result = await _dbContext.SaveChangesAsync();
                    return result > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
