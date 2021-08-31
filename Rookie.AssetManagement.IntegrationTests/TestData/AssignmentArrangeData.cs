using Rookie.AssetManagement.Business;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using Rookie.AssetManagement.Contracts.Enums;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.IntegrationTests.TestData
{
    public class AssignmentArrangeData
    {
        public static async Task InitAssignmentDataAsync(BaseRepository<Assignment> assignmentRepository)
        {
            if (!assignmentRepository.GetAll().Result.Any())
            {
                var assignmentList = new List<Assignment>()
                {
                    new Assignment
                    {
                        Id = 1,
                        AssignById = 3,
                        AssignToId = 1,
                        AssignedDate = DateTime.Now,
                        State = AssignmentState.Completed,
                        Note = "Demo",  
                        AssetId = 1,                       
                    },
                    new Assignment
                    {
                        Id =2 ,
                        AssignById = 3,
                        AssignToId = 1,
                        AssignedDate = DateTime.Now,
                        State = AssignmentState.Accepted,
                        Note = "Demo",
                        AssetId = 1,
                    },
                    new Assignment
                    {
                        Id = 3 ,
                        AssignById = 3,
                        AssignToId = 1,
                        AssignedDate = DateTime.Now,
                        State = AssignmentState.WaitingForAcceptance,
                        Note = "Demo",
                        AssetId = 1,
                    }
                };
                foreach (Assignment assignment in assignmentList)
                {
                    await assignmentRepository.Add(assignment);
                }
            }

        }

        public static AssignmentQueryCriteriaDto GetAssignmentQueryCriteraDto()
        {
            return new AssignmentQueryCriteriaDto
            {
                Page = 1,
                Limit = 5,
                SortColumn = "id"
            };
        }

        public static AssignmentCreateDto GetAssignmentCreateDto()
        {
            return new AssignmentCreateDto
            {
                AssetId = 1,
                AssignToId = 1,
                Note = "Note",
                AssignedDate = DateTime.Now.AddHours(1)
            };
        }
    }
}
