using Rookie.AssetManagement.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Contracts.Dtos.AssetDtos
{
    public class AssetQueryCriteriaDto : BaseQueryCriteria
    {
        public AssetState[] State { get; set; }
        public string[] CategoryName { get; set; }
        public Location Location { get; set; }
    }
}
