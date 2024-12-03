using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.ViewModels
{
    public class PublicationVM
    {
        public int Id { get; set; }
        public string Information { get; set; }
        public string CreatedAt { get; set; }
        public LightUserVM CreatedBy { get; set; }
        public EnterpriseVM Enterprise { get; set; }
        public double Ranking { get; set; }
        public List<AttachmentVM> Attachments { get; set; }
        public List<CommentVM> Comments { get; set; }
    }
}
