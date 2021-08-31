using Rookie.AssetManagement.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos
{
    public class AssignmentDto
    {
        public int Id { get; set; }
        public DateTime AssignedDate { get; set; }
        public AssignmentState State { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string AssignedBy { get; set; }
        public string AssignedTo { get; set; }
    }
}
