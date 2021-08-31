using Rookie.AssetManagement.Contracts.Enums;
using System;


namespace Rookie.AssetManagement.DataAccessor.Entities
{
    public class ReturnRequest
    {
        public int Id { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public ReturnRequestState State { get; set; }
        public int RequestedByUserId { get; set; }
        public int AcceptedByUserId { get; set; }
        public virtual User RequestedUser { get; set; }
        public virtual User AcceptedUser { get; set; }
        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }
    }
}
