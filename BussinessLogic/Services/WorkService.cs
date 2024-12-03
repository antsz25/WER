using BussinessLogic.Interfaces;
using BussinessLogic.Mappers;
using BussinessLogic.ViewModels;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    public class WorkService : IWorkService
    {
        private readonly IWorkRepository _workRepository;
        public WorkService(IWorkRepository workRepository)
        {
            _workRepository = workRepository;
        }
        public async Task<List<WorkVM>> GetWorksOfUser(int userId)
        {
            List<Work> works = await _workRepository.GetWorksOfUser(userId);
            return works.ToViewList();
        }

        public async Task<bool> SaveWork(WorkVM workVM)
        {
            Work Entity = workVM.ToEntity();
            return await _workRepository.SaveWork(Entity);
        }

        public async Task<bool> UpdateWork(WorkVM workVM)
        {
            Work Entity = workVM.ToEntity();
            return await _workRepository.UpdateWork(Entity);
        }
    }
}
