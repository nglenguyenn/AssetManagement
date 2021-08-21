using Rookie.AssetManagement.Contracts.Enums;
using System;

namespace Rookie.AssetManagement.Contracts.Dtos.AssetDtos
{
    public class AssetCreateDto
    {
        public string AssetName { get; set; }
        public string Specification { get; set; }
        public DateTime InstalledDate { get; set; }
        public AssetState State { get; set; }
        public int CategoryId { get; set; }
    }
}
