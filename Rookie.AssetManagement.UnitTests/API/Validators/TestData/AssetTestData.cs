using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Contracts.Enums;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.UnitTests.API.Validators.TestData
{
    public class AssetTestData
    {
        public static IEnumerable<object[]> ValidAssetName()
        {
            return new object[][]
            {
                new object[] { "Asus ProArt Monitor" }
            };
        }

        public static IEnumerable<object[]> InvalidAssetName()
        {
            return new object[][]
            {
                new object[]
                {
                    null,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(AssetCreateDto.AssetName))
                },
                new object[]
                {
                    "   ",
                   string.Format(ErrorTypes.Common.RequiredError, nameof(AssetCreateDto.AssetName))
                }
            };
        }

        public static IEnumerable<object[]> ValidCategoryId()
        {
            return new object[][]
            {
                new object[] { 1 }
            };
        }

        public static IEnumerable<object[]> InvalidCategoryId()
        {
            return new object[][]
            {
                new object[]
                {
                    0,
                    string.Format(ErrorTypes.Common.NumberGreaterThanError, nameof(AssetCreateDto.CategoryId), 0)
                },
                new object[]
                {
                    -1,
                    string.Format(ErrorTypes.Common.NumberGreaterThanError, nameof(AssetCreateDto.CategoryId), 0)
                }
            };
        }

        public static IEnumerable<object[]> ValidSpecification()
        {
            return new object[][]
            {
                new object[] { "Monitor's specification" }
            };
        }

        public static IEnumerable<object[]> InvalidSpecification()
        {
            return new object[][]
            {
                new object[]
                {
                    null,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(AssetCreateDto.Specification))
                },
                new object[]
                {
                    "   ",
                   string.Format(ErrorTypes.Common.RequiredError, nameof(AssetCreateDto.Specification))
                }
            };
        }

        public static IEnumerable<object[]> ValidInstalledDate()
        {
            return new object[][]
            {
                new object[] { new DateTime(2021, 8, 1) }
            };
        }

        public static IEnumerable<object[]> InvalidInstalledDate()
        {
            return new object[][]
            {
                new object[]
                {
                    DateTime.MinValue,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(AssetCreateDto.InstalledDate))
                }
            };
        }

        public static IEnumerable<object[]> ValidState()
        {
            return new object[][]
            {
                new object[] { AssetState.Assigned },
                new object[] { AssetState.Available },
                new object[] { AssetState.NotAvailable },
                new object[] { AssetState.Recycled },
                new object[] { AssetState.WaitingForApproval },
                new object[] { AssetState.WaitingForRecycling }
            };
        }

        public static IEnumerable<object[]> InvalidState()
        {
            return new object[][]
            {
                new object[]
                {
                    NotAssetState.NotState,
                    string.Format(ErrorTypes.Common.InvalidEnumError, int.MaxValue, nameof(AssetCreateDto.State))
                }
            };
        }

       public static AssetCreateDto GetAssetCreateDto(int categoryId)
       {
           return new AssetCreateDto
           {
               AssetName = "Dell XPS 13",
               Specification = "Intel® Core™ i7 1165G7 (2.8GHz upto 4.7Ghz, 12MB cache)",
               InstalledDate = new DateTime(),
               State = AssetState.Available,
               CategoryId = categoryId
           };
       }

        public static List<Category> GetCategories()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Laptop",
                    CategoryCode = "LA"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Monitor",
                    CategoryCode = "MO"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Personal Computer",
                    CategoryCode = "PC"
                }
            };
        }

        public static Asset GetAsset(int id, string assetCode, int categoryId)
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
    }
}
