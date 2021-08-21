using Rookie.AssetManagement.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Contracts.Dtos.AssetDtos
{
    public class AssetResponseDto
    {
        public int Id { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string Specification { get; set; }
        public DateTime InstalledDate { get; set; }
        public AssetState State { get; set; }
        public string Location { get; set; }
        public int CategoryId { get; set; }
    }
}
