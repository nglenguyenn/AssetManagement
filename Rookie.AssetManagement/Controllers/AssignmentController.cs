using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;
        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet]
        [Route("getAssignments")]
        public async Task<IActionResult> GetAssignments([FromQuery] AssignmentQueryCriteriaDto assignmentQueryCriteria,
            CancellationToken cancellationToken)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return await _assignmentService.GetAssignmentListAsync(identity, assignmentQueryCriteria, cancellationToken);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignmentById(int id)
        {
            var response = await _assignmentService.GetAssignmentByIdAsync(id);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAssignment([FromBody] AssignmentCreateDto assignmentCreateDto,
            CancellationToken cancellationToken)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return await _assignmentService.CreateAssignmentAsync(identity, assignmentCreateDto, cancellationToken);
        }
    }
}
