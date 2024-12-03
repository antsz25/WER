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
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IEnterpriseRepository _enterpriseRepository;
        public EnterpriseService(IEnterpriseRepository enterpriseRepository)
        {
            _enterpriseRepository = enterpriseRepository;
        }
        public async Task<List<EnterpriseVM>> GetEnterprises()
        {
            List<Enterprise> enterprisesEntities = await _enterpriseRepository.GetEnterprises();
            return enterprisesEntities.ToViewList();
        }

        public async Task<bool> SaveEnterprise(EnterpriseVM enterprise)
        {
            Enterprise entity = enterprise.ToEntity();
            return await _enterpriseRepository.SaveEnterprise(entity);
        }
    }
}
