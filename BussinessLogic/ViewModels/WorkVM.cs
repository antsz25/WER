using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.ViewModels
{
    public class WorkVM
    {
        public int Id { get; set; }
        public EnterpriseVM Enterprise { get; set; }
        public int UserId { get; set; }
        public string StartDate { get; set; }
        public string? EndDate { get; set; }
    }
}
