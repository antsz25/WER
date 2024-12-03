using BussinessLogic.ViewModels;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Mappers
{
    public static class WorkMapper
    {
        public static List<WorkVM> ToViewList(this ICollection<Work> listEntities)
        {
            return listEntities.Select(x => x.ToView()).ToList();
        }
        public static WorkVM ToView(this Work entity)
        {
            return new WorkVM
            {
                Id = entity.Id,
                Enterprise = entity.Enterprise.ToView(),
                UserId = entity.UserId,
                StartDate = entity.StartDate.ToShortDateString(),
                EndDate = entity.EndDate.HasValue ? entity.EndDate.Value.ToShortDateString() : null,
            };
        }
        public static ICollection<Work> ToListEntity(this List<WorkVM> listViews)
        {
            return listViews.Select(x => x.ToEntity()).ToList();
        }
        public static Work ToEntity(this WorkVM work)
        {
            return new Work
            {
                Id = work.Id,
                Enterprise = work.Enterprise.ToEntity(),
                UserId = work.UserId,
                StartDate = DateTime.Parse(work.StartDate),
                EndDate = work.EndDate != null ? DateTime.Parse(work.EndDate) : null,
            };
        }
    }
}
