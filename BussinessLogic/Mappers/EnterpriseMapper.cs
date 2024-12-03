using BussinessLogic.ViewModels;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Mappers
{
    public static class EnterpriseMapper
    {
        public static List<EnterpriseVM> ToViewList(this ICollection<Enterprise> entities)
        {
            return entities.Select(x => x.ToView()).ToList();
        }
        public static EnterpriseVM ToView(this Enterprise entity)
        {
            return new EnterpriseVM
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
        public static ICollection<Enterprise> ToEntityList(this List<EnterpriseVM> enterprises)
        {
            return enterprises.Select(x => x.ToEntity()).ToList();
        }
        public static Enterprise ToEntity(this EnterpriseVM enterprise)
        {
            return new Enterprise
            {
                Id = enterprise.Id,
                Name = enterprise.Name,
            };
        }
    }
}
