using Rookie.AssetManagement.Contracts.Enums;
using System;

namespace Rookie.AssetManagement.DataAccessor.Entities
{
    public class Assignment
    {
        public int Id { get; set; }
        public DateTime AssignedDate { get; set; }
        public AssignmentState State { get; set; }
        public string Note { get; set; }

        public int AssetId { get; set; }
        public virtual Asset Asset { get; set; }

        public int AssignById { get; set; }
        public virtual User AssignBy { get; set; }

        public int AssignToId { get; set; }
        public virtual User AssignTo { get; set; }

        public virtual ReturnRequest ReturnRequest { get; set; }
    }
}
