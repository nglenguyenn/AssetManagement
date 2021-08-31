using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using Rookie.AssetManagement.Contracts.Enums;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Helper
{
    public static class AssignmentServiceHelper
    {
        public static IQueryable<Assignment> AssignmentListFilter(IQueryable<Assignment> assignments,
            AssignmentQueryCriteriaDto query)
        {
            var assignmentsResult = assignments
                .Where(assign => assign.Asset.Location.Equals(query.Location.ToString()));

            if(!string.IsNullOrEmpty(query.Search))
            {
                var search = query.Search;
                assignmentsResult = assignmentsResult
                    .Where(assign => 
                        assign.Asset.AssetCode.Contains(search) ||
                        assign.Asset.AssetName.Contains(search) ||
                        assign.AssignTo.UserName.Contains(search));
            }

            if(query.States != null &&
                query.States.Count() > 0 &&
                !query.States.Any(x => x.ToString() == AssignmentStateFilter.ALL))
            {
                assignmentsResult = assignmentsResult
                    .Where(assign => query.States.Contains(assign.State));
            }

            if(query.AssignedDate != DateTime.MinValue)
            {
                var startOfDay = query.AssignedDate.Date;
                var endOfDay = query.AssignedDate.Date.AddDays(1).AddTicks(-1);
                assignmentsResult = assignmentsResult
                    .Where(assign => assign.AssignedDate >= startOfDay && assign.AssignedDate <= endOfDay);
            }

            return assignmentsResult;
        }
    }
}
