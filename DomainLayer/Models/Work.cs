using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Work
    {
        public int Id { get; set; }
        public int EnterpriseId { get; set; }
        public int UserId {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [ForeignKey("EnterpriseId")]
        public virtual Enterprise Enterprise { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
