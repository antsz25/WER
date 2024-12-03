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
    public class PublicationRepository : Repository<Publication>, IRepository<Publication>, IPublicationRepository
    {
        public PublicationRepository(MainContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Publication>> GetExperiences()
        {
            try
            {
                return await _dbContext.Set<Publication>()
                                            .Include(x => x.Attachments)
                                            .Include(x => x.Comments)
                                            .Include(x => x.User)
                                            .Include(x => x.Enterprise).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Publication>> GetExperiencesByUser(int userId)
        {
            try
            {
                return await _dbContext.Set<Publication>()
                                            .Include(x => x.Attachments)
                                            .Include(x => x.Comments)
                                            .Include(x => x.User)
                                            .Include(x => x.Enterprise)
                                            .Where(x => x.CreatedBy == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SaveExperience(Publication publication)
        {
            try
            {
                await _dbContext.Set<Publication>().AddAsync(publication);
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
