using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rookie.AssetManagement.Business.Helper;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using Rookie.AssetManagement.Contracts.Enums;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Services
{
    public class AssignmentService : ControllerBase, IAssignmentService
    {
        private readonly IBaseRepository<Assignment> _assignmentRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public AssignmentService(IBaseRepository<Assignment> assignmentRepository, IMapper mapper,
            UserManager<User> userManager)
        {
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> GetAssignmentListAsync(ClaimsIdentity identity,
            AssignmentQueryCriteriaDto assignmentQueryCriteria,
            CancellationToken cancellationToken)
        {
            var claimsId = identity.FindFirst(UserClaims.Id).Value;
            var userIdentity = await _userManager.FindByIdAsync(claimsId);

            assignmentQueryCriteria.Location = userIdentity.Location;

            var assignments = _assignmentRepository.Entities
                .Include(x => x.Asset)
                .Include(x => x.AssignTo)
                .Include(x => x.AssignBy)
                .Where(x => x.State == AssignmentState.Accepted
                || x.State == AssignmentState.WaitingForAcceptance);

            var assignmentsResult = AssignmentServiceHelper
                .AssignmentListFilter(assignments, assignmentQueryCriteria);
            var pagingModel = await assignmentsResult
                .AsNoTracking()
                .PaginateAsync(assignmentQueryCriteria, cancellationToken);

            var assignmentDtoResult = _mapper.Map<IEnumerable<AssignmentDto>>(pagingModel.Items);

            return Ok(new PagedResponseModel<AssignmentDto>()
            {
                CurrentPage = pagingModel.CurrentPage,
                TotalItems = pagingModel.TotalItems,
                TotalPages = pagingModel.TotalPages,
                Items = assignmentDtoResult
            });
        }

        public async Task<AssignmentDetailDto> GetAssignmentByIdAsync(int id)
        {
            var assignment = await _assignmentRepository.GetByAsync(
                filter: x => x.Id == id,
                includeProperties: "Asset,AssignBy,AssignTo");

            if(assignment == null)
            {
                throw new NotFoundException(ErrorTypes.Assignment.AssignmentNotFoundError);
            }

            var assignmentDto = _mapper.Map<AssignmentDetailDto>(assignment);


            return assignmentDto;
        }

        public async Task<IActionResult> CreateAssignmentAsync(
            ClaimsIdentity identity,
            AssignmentCreateDto assignmentCreateDto,
            CancellationToken cancellationToken)
        {
            if (assignmentCreateDto == null)
                return BadRequest();

            var id = identity.FindFirst(UserClaims.Id).Value;
            if (id == null)
                return Unauthorized("Token not found");

            if (identity.FindFirst(UserClaims.Role).Value != UserConstants.Common.AdminRole)
                return Unauthorized();

            var newAssignment = _mapper.Map<Assignment>(assignmentCreateDto);
            newAssignment.AssignById = Int32.Parse(id);
            newAssignment.State = AssignmentState.WaitingForAcceptance;

            var addedAssignment = await _assignmentRepository.Add(newAssignment);
            var assignment = await _assignmentRepository.Entities
                .Include(x => x.Asset)
                .Include(x => x.AssignTo)
                .Include(x => x.AssignBy)
                .Where(x => x.Id == addedAssignment.Id).FirstOrDefaultAsync();
            var newAssignmentResponse = _mapper.Map<AssignmentDto>(assignment);

            return Ok(newAssignmentResponse);
        }
    }
}
