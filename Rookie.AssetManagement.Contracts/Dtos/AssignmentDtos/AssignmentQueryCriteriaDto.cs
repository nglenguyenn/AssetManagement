using Rookie.AssetManagement.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos
{
    public class AssignmentQueryCriteriaDto : BaseQueryCriteria
    {
        public AssignmentState[] States { get; set; }
        public DateTime AssignedDate { get; set; }
        public Location Location { get; set; }
    }
}
