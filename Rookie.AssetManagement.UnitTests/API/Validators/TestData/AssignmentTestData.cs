using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using Rookie.AssetManagement.Contracts.Enums;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;

namespace Rookie.AssetManagement.UnitTests.API.Validators.TestData
{
    public class AssignmentTestData
    {
        public static List<Assignment> GetAssignments()
        {
            return new List<Assignment>
            {
                new Assignment
                {
                    Id=1,
                    AssignById=3,
                    AssignToId=1,
                    AssignedDate= DateTime.Now,
                    State=AssignmentState.Completed,
                    Note="Demo",
                    AssetId=1,
                }
            };
        }

        public static Assignment GetAssignment(int id, AssignmentState state)
        {
            return new Assignment
            {
                Id = id,
                AssignedDate = new DateTime(2021, 9, 2),
                Note = "note something",
                State = state,
            };
        }

        public static AssignmentQueryCriteriaDto GetAssignmentQueryCriteriaDto()
        {
            return new AssignmentQueryCriteriaDto
            {
                Page = 1,
                Limit = 5,
                SortColumn = "id",
                States = new AssignmentState[]
                {
                    AssignmentState.Accepted,
                    AssignmentState.WaitingForAcceptance
                },
                AssignedDate = new DateTime(2020, 8, 1),
                Search = "LA"
            };
        }

	public static IEnumerable<object[]> ValidAssetId()
        {
            return new object[][]
            {
                new object[] { 1 }
            };
        }
        public static IEnumerable<object[]> InvalidAssetId()
        {
            return new object[][]
            {
                new object[] 
                { 
                    -1,
                    string.Format(ErrorTypes.Common.RequiredError,nameof(AssignmentCreateDto.AssetId))
                }
            };
        }

        public static IEnumerable<object[]> ValidAssignToId()
        {
            return new object[][]
            {
                new object[] { 1 }
            };
        }

        public static IEnumerable<object[]> InvalidAssignToId()
        {
            return new object[][]
            {
                new object[]
                {
                    -1,
                    string.Format(ErrorTypes.Common.RequiredError,nameof(AssignmentCreateDto.AssignToId))
                }
            };
        }

        public static IEnumerable<object[]> ValidDate()
        {
            return new object[][]
            {
                new object[] { DateTime.Now }
            };
        }
    }
}
