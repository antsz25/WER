using BussinessLogic.ViewModels;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
    public interface IPublicationService
    {
        public Task<List<PublicationVM>> GetExperiences();
        public Task<List<PublicationVM>> GetExperiencesByUser(int userId);
        public Task<bool> SaveExperience(PublicationVM publication);
    }
}
