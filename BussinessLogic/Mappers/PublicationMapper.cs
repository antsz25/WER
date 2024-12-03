using BussinessLogic.ViewModels;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Mappers
{
    public static class PublicationMapper
    {
        public static List<PublicationVM> ToViewList(this ICollection<Publication> publications)
        {
            return publications.Select(x => x.ToView()).ToList();
        }
        public static PublicationVM ToView(this Publication publication)
        {
            return new PublicationVM
            {
                Id = publication.Id,
                CreatedBy = publication.User.ToViewLight(),
                CreatedAt = publication.CreatedAt.ToShortDateString(),
                Ranking = publication.Ranking,
                Information = publication.Information,
                Attachments = publication.Attachments != null ? publication.Attachments.ToViewList() : new List<AttachmentVM>(),
                Comments = publication.Comments != null ? publication.Comments.ToViewList() : new List<CommentVM>(),
                Enterprise = publication.Enterprise.ToView()
            };
        }
        public static List<Publication> ToEntityList(this List<PublicationVM> publicationVMs)
        {
            return publicationVMs.Select(x => x.ToEntity()).ToList();
        }
        public static Publication ToEntity(this PublicationVM publicationVM)
        {
            return new Publication
            {
                Id = publicationVM.Id,
                CreatedBy = publicationVM.CreatedBy.Id,
                Attachments = publicationVM.Attachments.ToEntityList(),
                Comments = publicationVM.Comments.ToEntityList(),
                EnterpriseId = publicationVM.Enterprise.Id,
                Information = publicationVM.Information,
                Ranking = publicationVM.Ranking,
            };
        }
    }
}
