using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using Rookie.AssetManagement.Business;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Business.Services;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using MockQueryable.Moq;
using System.Threading;
using Rookie.AssetManagement.UnitTests.API.Validators.TestData;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using Rookie.AssetManagement.Contracts.Enums;
using Rookie.AssetManagement.Contracts.Constants;

namespace Rookie.AssetManagement.UnitTests.Business
{
    public class AssignmentServiceShould
    {
        private readonly AssignmentService _assignmentService;
        private readonly Mock<IBaseRepository<Asset>> _assetRepository;
        private readonly Mock<IBaseRepository<ReturnRequest>> _returnRequestRepository;
        private readonly Mock<IBaseRepository<Assignment>> _assignmentRepository;
        private readonly UserManager<User> _userManager;
        private readonly Mock<IUserStore<User>> _userStore;
        private readonly Mock<IPasswordHasher<User>> _passwordHasher;
        private readonly IMapper _mapper;

        public AssignmentServiceShould()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();
            _assetRepository = new Mock<IBaseRepository<Asset>>();
            _assignmentRepository = new Mock<IBaseRepository<Assignment>>();
            _returnRequestRepository = new Mock<IBaseRepository<ReturnRequest>>();
            _userStore = new Mock<IUserStore<User>>();
            _passwordHasher = new Mock<IPasswordHasher<User>>();

            var passwordManager = _userStore.As<IUserPasswordStore<User>>()
                .Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Success);
            var roleManager = _userStore.As<IUserRoleStore<User>>()
                .Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            _userManager = new UserManager<User>(_userStore.Object, null,
                _passwordHasher.Object, null, null, null, null, null, null);
            _assetRepository = new Mock<IBaseRepository<Asset>>();
            _assignmentService = new AssignmentService(
                _assignmentRepository.Object,
                _mapper,
                _userManager
                );
        }

        [Fact]
        public async Task ValidQueryShouldBeSuccess()
        {
            //Arrange
            var assign = AssignmentTestData.GetAssignment(1, AssignmentState.Accepted);
            assign.Asset = AssetTestData.GetAsset(1, "LA00001", 1);
            assign.AssignBy = UserTestData.GetUser(1, "SD0001");
            assign.AssignTo = UserTestData.GetUser(3, "SD0003");
            assign.AssignedDate = new DateTime(2020, 8, 1);
            var assignments = new List<Assignment>();
            assignments.Add(assign);
            _userStore.Setup(x => x.FindByIdAsync("1", CancellationToken.None))
                .ReturnsAsync(new User { Id = 1, Type = "ADMIN" });
            _assignmentRepository
               .Setup(x => x.Entities)
               .Returns(assignments.AsQueryable().BuildMock().Object);
            var identity = UserTestData.GetClaims();
            var assignmentQueryCriteria = AssignmentTestData.GetAssignmentQueryCriteriaDto();

            //Action
            ActionResult<PagedResponseModel<AssignmentDto>> result =
                (ActionResult)await _assignmentService
                .GetAssignmentListAsync(identity, assignmentQueryCriteria, CancellationToken.None);                   
            result.Should().NotBeNull();
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var pagedResponse = Assert.IsType<PagedResponseModel<AssignmentDto>>(actionResult.Value);
            Assert.Equal(1, pagedResponse.TotalItems);
            Assert.Equal(1, pagedResponse.TotalPages);
            pagedResponse.Items.Should()
                .NotBeEmpty()
                .And.HaveCount(1);
        }

        [Fact]
        public async Task ValidAssignmentIdShouldBeSuccess()
        {
            //Arrage
            int id = 1;
            var assign = AssignmentTestData.GetAssignment(1, AssignmentState.Accepted);
            assign.Asset = AssetTestData.GetAsset(1, "LA00001", 1);
            assign.AssignBy = UserTestData.GetUser(1, "SD0001");
            assign.AssignTo = UserTestData.GetUser(3, "SD0003");
            assign.AssignedDate = new DateTime(2020, 8, 1);

            var assignments = new List<Assignment>();
            assignments.Add(assign);

            _assignmentRepository
                .Setup(x => x.GetByAsync(
                     x => x.Id == id,
                 "Asset,AssignBy,AssignTo"))
                .ReturnsAsync(assignments.AsQueryable().FirstOrDefault());


            //Action
            var result = await _assignmentService.GetAssignmentByIdAsync(id);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(assign.Id, result.Id);
            Assert.Equal(assign.AssignedDate, result.AssignedDate);
            Assert.Equal(assign.Note, result.Note);
            Assert.Equal(assign.Asset.AssetCode, result.AssetCode);
            Assert.Equal(assign.Asset.AssetName, result.AssetName);
            Assert.Equal(assign.AssignBy.UserName, result.AssignedBy);
            Assert.Equal(assign.AssignTo.UserName, result.AssignedTo);
            Assert.Equal(assign.Asset.Specification, result.Specification);
        }
        [Fact]
        public async Task ValidCreateAssignmentShouldBeSuccess()
        {
            //Arrange
            var assign = AssignmentTestData.GetAssignment(1, AssignmentState.Accepted);
            assign.Asset = AssetTestData.GetAsset(1, "LA00001", 1);
            assign.AssignBy = UserTestData.GetUser(1, "SD0001");
            assign.AssignTo = UserTestData.GetUser(1, "SD0003");
            assign.AssignedDate = new DateTime(2020, 8, 1);
            var assignments = new List<Assignment>();
            assignments.Add(assign);

            _assignmentRepository
               .Setup(x => x.Entities)
               .Returns(assignments.AsQueryable().BuildMock().Object);

            var assignment = new AssignmentCreateDto
            {
                AssetId = 1,
                AssignedDate = DateTime.Now.AddDays(1),
                AssignToId = 1,
                Note = "Note"
            };
            _assignmentRepository
                .Setup(x => x.Add(It.IsAny<Assignment>()))
                .ReturnsAsync(assign);

            var identity = UserTestData.GetClaims();

            //Action
            ActionResult<AssignmentCreateDto> result =
                (ActionResult)await _assignmentService
                    .CreateAssignmentAsync(identity, assignment, CancellationToken.None);
            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var isTypeResult = Assert.IsType<AssignmentDto>(actionResult.Value);
        }

        [Fact]
        public async Task InvalidAssignmentIdShouldBeUnknown()
        {
            //Arrage
            int id = 12;
            var assign = AssignmentTestData.GetAssignment(1, AssignmentState.Accepted);
            assign.Asset = AssetTestData.GetAsset(1, "LA00001", 1);
            assign.AssignBy = UserTestData.GetUser(1, "SD0001");
            assign.AssignTo = UserTestData.GetUser(3, "SD0003");
            assign.AssignedDate = new DateTime(2020, 8, 1);

            var assignments = new List<Assignment>();
            assignments.Add(assign);

            _assignmentRepository
                .Setup(x => x.Entities)
                .Returns(assignments.AsQueryable().BuildMock().Object); 

            //Action
            Func<Task> result = () =>  _assignmentService.GetAssignmentByIdAsync(id);

            //Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(result);

            Assert.Contains(ErrorTypes.Assignment.AssignmentNotFoundError, exception.Message); 
                        
        }

        [Fact]
        public async Task NullCreateAssignmentShouldBeBadRequest()
        {
            //Arrange
            AssignmentCreateDto assignment = null;
            _assignmentRepository
                .Setup(x => x.Add(It.IsAny<Assignment>()))
                .ReturnsAsync(new Assignment());
            var identity = UserTestData.GetClaims();
            //Action
            ActionResult<AssignmentCreateDto> result =
                (ActionResult)await _assignmentService
                    .CreateAssignmentAsync(identity, assignment, CancellationToken.None);
            //Assert
            var actionResult = Assert.IsType<BadRequestResult>(result.Result);
        }
    }
}
