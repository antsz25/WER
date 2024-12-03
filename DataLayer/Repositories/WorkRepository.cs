using Arch.EntityFrameworkCore.UnitOfWork;
using DataLayer.Context;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class WorkRepository : Repository<Work>, IRepository<Work>, IWorkRepository
    {
        public WorkRepository(MainContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Work>> GetWorksOfUser(int userId)
        {
            try
            {
                return await _dbContext.Set<Work>()
                                        .Include(x => x.User)
                                        .Include(X => X.Enterprise)
                                        .Where(x => x.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SaveWork(Work work)
        {
            try
            {
                Enterprise? exists = await _dbContext.Set<Enterprise>().FirstOrDefaultAsync(x => x.Name.ToLower().Equals(work.Enterprise.Name.ToLower()));
                if (exists == null)
                {
                    work.Enterprise.Id = 0;
                    await _dbContext.Set<Enterprise>().AddAsync(work.Enterprise);            
                }
                else
                {
                    work.Enterprise = exists;
                }
                _dbContext.ChangeTracker.DetectChanges();
                await _dbContext.Set<Work>().AddAsync(work);
                int result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateWork(Work work)
        {
            try
            {
                Work? originalWork = await _dbContext.Set<Work>()
                    .Include(x => x.User)
                    .Include(x => x.Enterprise)
                    .FirstOrDefaultAsync(x => x.Id == work.Id);
                if (originalWork != null)
                {
                    foreach (PropertyInfo property in originalWork.GetType().GetProperties())
                    {
                        if (
                            property.Name != "Id" &&
                            property.Name != "User" &&
                            property.Name != "Enterprise" &&
                            property.Name != "UserId" &&
                            property.Name != "EnterpriseId"
                           )
                        {
                            object? originalValue = property.GetValue(originalWork);
                            object? updatedValue = property.GetValue(work);
                            if (originalValue.ToString() != updatedValue.ToString())
                            {
                                originalValue = updatedValue;
                                _dbContext.Entry(originalWork).Property(property.Name).IsModified = true;
                            }
                        }
                    }
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
