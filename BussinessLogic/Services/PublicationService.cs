using BussinessLogic.Interfaces;
using BussinessLogic.Mappers;
using BussinessLogic.ViewModels;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    public class PublicationService : IPublicationService
    {
        private readonly IPublicationRepository _publicationRepository;
        public PublicationService(IPublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }

        public async Task<List<PublicationVM>> GetExperiences()
        {
            List<Publication> entities = await _publicationRepository.GetExperiences();
            return entities.ToViewList();
        }

        public async Task<List<PublicationVM>> GetExperiencesByUser(int userId)
        {
            List<Publication> entities = await _publicationRepository.GetExperiencesByUser(userId);
            return entities.ToViewList();
        }

        public async Task<bool> SaveExperience(PublicationVM publication)
        {
            publication.Attachments.ForEach(file =>
            {
                file.Base64 = Regex.Replace(
                                    file.Base64,
                                    @"data:[a-z]+\/[a-z]+;base64,",
                                    string.Empty
                                );
                file.UploadedBy = publication.CreatedBy.Id;
                file.PublicationId = publication.Id;
            });
            Publication publicationEntity = publication.ToEntity();
            return await _publicationRepository.SaveExperience(publicationEntity);
        }
    }
}
