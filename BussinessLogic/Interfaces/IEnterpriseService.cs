using BussinessLogic.ViewModels;
using DomainLayer.Models;

namespace BussinessLogic.Interfaces
{
    public interface IEnterpriseService
    {
        public Task<List<EnterpriseVM>> GetEnterprises();
        public Task<bool> SaveEnterprise(EnterpriseVM enterprise);
    }
}
