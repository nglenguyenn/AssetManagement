using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Business;
using Rookie.AssetManagement.Business.Services;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using Rookie.AssetManagement.Controllers;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.IntegrationTests.Common;
using Rookie.AssetManagement.IntegrationTests.TestData;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Rookie.AssetManagement.IntegrationTests
{
    public class AssignmentControllerShould : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly BaseRepository<Asset> _assetRepository;
        private readonly BaseRepository<Category> _categoryRepository;
        private readonly BaseRepository<ReturnRequest> _returnRequestRepository;
        private readonly BaseRepository<Assignment> _assignmentRepository;
        private readonly AssignmentService _assignmentService;
        private readonly AssignmentController _assignmentController;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        public AssignmentControllerShould(SqliteInMemoryFixture fixture)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            fixture.CreateDatabase();
            _dbContext = fixture.Context;
            _userManager = fixture.UserManager;
            _roleManager = fixture.RoleManager;
            _assetRepository = new BaseRepository<Asset>(_dbContext);
            _categoryRepository = new BaseRepository<Category>(_dbContext);
            _assignmentRepository = new BaseRepository<Assignment>(_dbContext);
            _returnRequestRepository = new BaseRepository<ReturnRequest>(_dbContext);
            _assignmentService = new AssignmentService(_assignmentRepository, _mapper, _userManager);
            _assignmentController = new AssignmentController(_assignmentService);
            RoleArrangeData.InitRolesDataAsync(_roleManager).Wait();
            UserArrangeData.InitUsersDataAsync(_userManager).Wait();
            AssetArrangeData.InitAssetsDataAsync(_assetRepository).Wait();
            AssignmentArrangeData.InitAssignmentDataAsync(_assignmentRepository).Wait();
            ReturnRequestArrangeData.InitReturnRequestDataAsync(_returnRequestRepository).Wait();
        }

        [Fact]
        public async Task GetAssignmentsShouldBeSuccess()
        {
            //Arrage
            var assignmentQueryCriteria = AssignmentArrangeData.GetAssignmentQueryCriteraDto();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(UserClaims.Id, "1"),
                new Claim(UserClaims.StaffCode, "SD0000"),
                new Claim(UserClaims.FullName, "Tran Cuong"),
                new Claim(UserClaims.Location, "HCM"),
                new Claim(UserClaims.Role, "ADMIN")
            }, "mock"));
            _assignmentController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            //Action
            ActionResult<PagedResponseModel<AssignmentDto>> result =
                (ActionResult) await _assignmentController
                                    .GetAssignments(assignmentQueryCriteria, CancellationToken.None);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var pagedResponse = Assert.IsType<PagedResponseModel<AssignmentDto>>(actionResult.Value);
            Assert.Equal(2, pagedResponse.TotalItems);
            Assert.Equal(1, pagedResponse.TotalPages);
            pagedResponse.Items.Should()
                .NotBeEmpty()
                .And.HaveCount(2);
        }

        [Fact]
        public async Task GetAssignmentSuccess()
        {
            //Arrage
            var id = 1;

            //Action
            ActionResult<AssignmentDetailDto> result = (ActionResult)await _assignmentController.GetAssignmentById(id);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var assignmentDetailDto = Assert.IsType<AssignmentDetailDto>(actionResult.Value);

            Assert.NotNull(assignmentDetailDto);
        }
	[Fact]
        public async Task CreateAssignmentsShouldBeSuccess()
        {
            //Arrange
            var newAssignment = AssignmentArrangeData.GetAssignmentCreateDto();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(UserClaims.Id, "1"),
                new Claim(UserClaims.StaffCode, "SD0000"),
                new Claim(UserClaims.FullName, "Tran Cuong"),
                new Claim(UserClaims.Location, "HCM"),
                new Claim(UserClaims.Role, "ADMIN")
            }, "mock"));
            _assignmentController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            //Action
            ActionResult<AssignmentCreateDto> result =
                (ActionResult)await _assignmentController
                                    .CreateAssignment(newAssignment, CancellationToken.None);
            //Arrange
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
