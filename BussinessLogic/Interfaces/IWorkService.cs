using BussinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
    public interface IWorkService
    {
        public Task<List<WorkVM>> GetWorksOfUser(int userId);
        public Task<bool> SaveWork(WorkVM workVM);
        public Task<bool> UpdateWork(WorkVM workVM);
    }
}
