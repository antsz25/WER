using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces
{
    public interface IWorkRepository
    {
        public Task<List<Work>> GetWorksOfUser(int userId);
        public Task<bool> SaveWork(Work work);
        public Task<bool> UpdateWork(Work work);
    }
}
