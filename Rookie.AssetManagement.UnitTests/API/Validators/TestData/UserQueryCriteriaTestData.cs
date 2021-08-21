using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using Rookie.AssetManagement.Contracts.EnumDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.UnitTests.API.Validators.TestData
{
    public static class UserQueryCriteriaTestData
    {
        public static IEnumerable<object[]> ValidSortOrder()
        {
            return new object[][]
            {
                new object[] { SortOrderEnumDto.Accsending },
                new object[] { SortOrderEnumDto.Decsending },
            };
        }

        public static IEnumerable<object[]> InvalidSortOrder()
        {
            return new object[][]
            {
                new object[] { 
                    NotSortOrderEnum.NotAscending,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(UserQueryCriteriaDto.SortOrder))
                },
                new object[] {
                    NotSortOrderEnum.NotDescending,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(UserQueryCriteriaDto.SortOrder))
                }
            };
        }

        public static IEnumerable<object[]> ValidSortColumn()
        {
            return new object[][]
            {
                new object[] { "Username" },
                new object[] { "Type" },
            };
        }

        public static IEnumerable<object[]> InvalidSortColumn()
        {
            return new object[][]
            {
                new object[] {
                    null,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(UserQueryCriteriaDto.SortColumn))
                }
            };
        }

        public static IEnumerable<object[]> ValidPage()
        {
            return new object[][]
            {
                new object[] { 1 },
                new object[] { 2 },
            };
        }

        public static IEnumerable<object[]> InvalidPage()
        {
            return new object[][]
            {
                new object[] {
                    0,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(UserQueryCriteriaDto.Page))
                },
                new object[] {
                    0,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(UserQueryCriteriaDto.Page))
                }
            };
        }
    }
}
