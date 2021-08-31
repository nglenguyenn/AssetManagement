using System;

namespace Rookie.AssetManagement.Contracts.Dtos.AssetDtos
{
    public class AssetHistoryDto
    {
        public int Id { get; set; }
        public DateTime AssignedDate { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedBy { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}