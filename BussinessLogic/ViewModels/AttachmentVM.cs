using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.ViewModels
{
    public class AttachmentVM
    {
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public int UploadedBy { get; set; }
        public string Base64 { get; set; }
        public string Url { get; set; }
        public string FileType { get; set; }
        public string Alt { get; set; }
    }
}
