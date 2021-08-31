using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Interfaces
{
    public interface IAssignmentService
    {
        public Task<IActionResult> GetAssignmentListAsync(ClaimsIdentity identity,
            AssignmentQueryCriteriaDto assignmentQueryCriteria,
            CancellationToken cancellationToken);
        public Task<AssignmentDetailDto> GetAssignmentByIdAsync(int id);
        public Task<IActionResult> CreateAssignmentAsync(
            ClaimsIdentity identity,
            AssignmentCreateDto assignmentCreateDto,
            CancellationToken cancellationToken);
    }
}
