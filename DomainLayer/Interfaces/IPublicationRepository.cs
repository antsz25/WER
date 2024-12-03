using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces
{
    public interface IPublicationRepository
    {
        public Task<List<Publication>> GetExperiences();
        public Task<List<Publication>> GetExperiencesByUser(int userId);
        public Task<bool> SaveExperience(Publication publication);
    }
}
