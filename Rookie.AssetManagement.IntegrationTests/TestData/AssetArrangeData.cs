using Rookie.AssetManagement.Business;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Contracts.EnumDtos;
using Rookie.AssetManagement.Contracts.Enums;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.IntegrationTests.TestData
{
    public class AssetArrangeData
    {
        public static async Task InitAssetsDataAsync(BaseRepository<Asset> assetRepository)
        {
            if (!assetRepository.GetAll().Result.Any())
            {
                var assetList = new List<Asset>()
                {
                    new Asset
                    {
                        Id = 1,
                        AssetName = "Asset 1 HCM",
                        AssetCode = "LA000000",
                        Specification = "Specification for Asset 2 HCM",
                        InstalledDate = DateTime.Now,
                        Location = "HCM",
                        State = AssetState.Available,
                        CategoryId = 1,
                        
                    },
                    new Asset
                    {
                        Id = 2,
                        AssetName = "Asset 2 HCM",
                        AssetCode = "MO000000",
                        Specification = "Specification for Asset 2 HCM",
                        InstalledDate = DateTime.Now,
                        Location = "HCM",
                        State = AssetState.Available,
                        CategoryId = 2,

                    },
                    new Asset
                    {
                        Id = 3,
                        AssetName = "Asset 3 HCM",
                        AssetCode = "PC000000",
                        Specification = "Specification for Asset 3 HCM",
                        InstalledDate = DateTime.Now,
                        Location = "HCM",
                        State = AssetState.Available,
                        CategoryId = 3,

                    },               
                    new Asset
                    {
                        Id = 4,
                        AssetName = "Asset 1 HN",
                        AssetCode = "LA000001",
                        Specification = "Specification for Asset 2 HCM",
                        InstalledDate = DateTime.Now,
                        Location = "HN",
                        State = AssetState.Available,
                        CategoryId = 1,

                    },
                    new Asset
                    {
                        Id = 5,
                        AssetName = "Asset 2 HN",
                        AssetCode = "MO000001",
                        Specification = "Specification for Asset 2 HN",
                        InstalledDate = DateTime.Now,
                        Location = "HN",
                        State = AssetState.Available,
                        CategoryId = 2,

                    },
                    new Asset
                    {
                        Id = 6,
                        AssetName = "Asset 3 HN",
                        AssetCode = "PC000001",
                        Specification = "Specification for Asset 3 HN",
                        InstalledDate = DateTime.Now,
                        Location = "HN",
                        State = AssetState.Available,
                        CategoryId = 3,

                    },
                };

                foreach (Asset asset in assetList)
                {
                    await assetRepository.Add(asset);                   
                }

            }
        }

        
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

        public static AssetEditDto GetAssetEditDto()
        {
            return new AssetEditDto()
            {
                Id = 1,
                AssetName = "Laptop Asus 5678",
                InstalledDate = new DateTime(2021, 8, 1),
                Specification = "This is the new specification",
                State = AssetState.Available
            };
        }

        public static Asset GetExistedAsset(int id, string assetCode, int categoryId)
        {
            return new Asset()
            {
                Id = id,
                AssetCode = assetCode,
                AssetName = "asset name",
                CategoryId = categoryId,
                InstalledDate = new DateTime(2021, 8, 1),
                Location = Location.HCM.ToString(),
                Specification = "specification",
                State = AssetState.Available
            };
        }

        public static AssetQueryCriteriaDto GetAssetQueryCriteraDto()
        {
            return new AssetQueryCriteriaDto()
            {
                State = new AssetState[] {AssetState.Available},
                Limit = 5,
                Location = Location.HCM,
                Page = 1,
                SortColumn = "Id",
                SortOrder = SortOrderEnumDto.Decsending
            };
        }
    }
}
