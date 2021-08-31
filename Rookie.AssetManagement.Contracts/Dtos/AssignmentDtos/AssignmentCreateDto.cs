using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos
{
    public class AssignmentCreateDto
    {
        public DateTime AssignedDate { get; set; }
        public int AssetId { get; set; }
        public int AssignToId { get; set; }
        public string Note { get; set; }

    }
}
