using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public int PublicationId {  get; set; }
        public int UploadedBy { get; set; }
        public byte[] Base64 { get; set; }
        public string Alt { get; set; }
        public string FileType { get; set; }
        [ForeignKey("PublicationId")]
        public virtual Publication Publication { get; set; }
        [ForeignKey("UploadedBy")]
        public virtual User User { get; set; }
    }
}
