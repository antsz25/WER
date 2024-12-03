using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.ViewModels
{
    public class CommentVM
    {
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string Message { get; set; }
    }
}
