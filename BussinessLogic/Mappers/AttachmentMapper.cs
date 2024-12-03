using BussinessLogic.ViewModels;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Mappers
{
    public static class AttachmentMapper
    {
        public static List<AttachmentVM> ToViewList(this ICollection<Attachment> attachments)
        {
            return attachments.Select(x => x.ToView()).ToList();
        }
        public static AttachmentVM ToView(this Attachment attachment)
        {
            return new AttachmentVM
            {
                Id = attachment.Id,
                Base64 = Convert.ToBase64String(attachment.Base64),
                FileType = attachment.FileType,
                PublicationId = attachment.PublicationId,
                UploadedBy = attachment.UploadedBy,
                Url = $"data:{attachment.FileType};base64,",
                Alt = attachment.Alt,
            };
        }
        public static ICollection<Attachment> ToEntityList(this List<AttachmentVM> attachments)
        {
            return attachments.Select(x => x.ToEntity()).ToList();
        }
        public static Attachment ToEntity(this AttachmentVM attachment)
        {
            return new Attachment
            {
                Id = attachment.Id,
                Alt = attachment.Alt,
                Base64 = Convert.FromBase64String(attachment.Base64),
                FileType = attachment.FileType,
                PublicationId = attachment.PublicationId,
                UploadedBy = attachment.UploadedBy,
            };
        }
    }
}
