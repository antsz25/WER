using BussinessLogic.ViewModels;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Mappers
{
    public static class CommentMapper
    {
        public static List<CommentVM> ToViewList(this ICollection<Comment> comments)
        {
            return comments.Select(x => x.ToView()).ToList();
        }
        public static CommentVM ToView(this Comment comment)
        {
            return new CommentVM
            {
                Id = comment.Id,
                CreatedAt = comment.CreatedAt.ToShortDateString(),
                CreatedBy = comment.CreatedBy,
                Message = comment.Message,
                PublicationId = comment.PublicationId,
            };
        }
        public static ICollection<Comment> ToEntityList(this List<CommentVM> comments)
        {
            return comments.Select(x => x.ToEntity()).ToList();
        }
        public static Comment ToEntity(this CommentVM comment)
        {
            return new Comment
            {
                Id = comment.Id,
                CreatedBy = comment.CreatedBy,
                Message = comment.Message,
                PublicationId = comment.PublicationId,
            };
        }
    }
}
