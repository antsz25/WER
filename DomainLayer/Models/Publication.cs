using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Publication
    {
        public int Id { get; set; }
        public int CreatedBy {  get; set; }
        public DateTime CreatedAt { get; set; }
        public int EnterpriseId {  get; set; }
        public double Ranking { get; set; }
        public string Information { get; set; }
        public ICollection<Attachment>? Attachments { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User User { get; set; }
        [ForeignKey("EnterpriseId")]
        public virtual Enterprise Enterprise { get; set; }
    }
}
