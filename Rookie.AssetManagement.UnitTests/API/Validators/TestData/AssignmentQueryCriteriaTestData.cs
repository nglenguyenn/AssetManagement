using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using Rookie.AssetManagement.Contracts.EnumDtos;
using Rookie.AssetManagement.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.UnitTests.API.Validators.TestData
{
    public static class AssignmentQueryCriteriaTestData
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
                    string.Format(ErrorTypes.Common.RequiredError, nameof(AssignmentQueryCriteriaDto.SortOrder))
                },
                new object[] {
                    NotSortOrderEnum.NotDescending,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(AssignmentQueryCriteriaDto.SortOrder))
                }
            };
        }

        public static IEnumerable<object[]> ValidSortColumn()
        {
            return new object[][]
            {
                new object[] { "state" },
                new object[] { "assetName" },
            };
        }

        public static IEnumerable<object[]> InvalidSortColumn()
        {
            return new object[][]
            {
                new object[] {
                    null,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(AssignmentQueryCriteriaDto.SortColumn))
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
                    string.Format(ErrorTypes.Common.RequiredError, nameof(AssignmentQueryCriteriaDto.Page))
                },
                new object[] {
                    0,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(AssignmentQueryCriteriaDto.Page))
                }
            };
        }

        public static IEnumerable<object[]> ValidState()
        {
            return new object[][]
            {
                new object[] 
                { 
                    new AssignmentState[] 
                    { 
                        AssignmentState.Accepted,
                        AssignmentState.WaitingForAcceptance
                    }
                },
                new object[] 
                {
                    new AssignmentState[]
                    {
                        AssignmentState.Accepted
                    }
                },
                new object[]
                {
                    new AssignmentState[]
                    {
                        AssignmentState.WaitingForAcceptance
                    }
                },
            };
        }

        public static IEnumerable<object[]> InvalidState()
        {
            return new object[][]
            {
                new object[] {
                    new AssignmentState[]
                    {
                        AssignmentState.Declined
                    },
                    string.Format(ErrorTypes.Assignment.StateFilter, nameof(AssignmentQueryCriteriaDto.States))
                },
                new object[] {
                    new AssignmentState[]
                    {
                        AssignmentState.Completed
                    },
                    string.Format(ErrorTypes.Assignment.StateFilter, nameof(AssignmentQueryCriteriaDto.States))
                }
            };
        }
    }
}
