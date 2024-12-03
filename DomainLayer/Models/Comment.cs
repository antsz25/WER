using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PublicationId {  get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; }
        [ForeignKey("PublicationId")]
        public virtual Publication Publication { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User User { get; set; }
    }
}
