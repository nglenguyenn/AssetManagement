using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.IntegrationTests.TestData
{
    public class AssetArrangeData
    {
        public static AssetCreateDto GetAssetCreateDto()
        {
            return new AssetCreateDto()
            {
                AssetName = "Laptop Asus 1234",
                CategoryId = 3,
                InstalledDate = new DateTime(2021, 8, 1),
                Specification = "",
                State = AssetState.Available
            };
        }
    }
}
