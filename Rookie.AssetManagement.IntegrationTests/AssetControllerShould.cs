using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Business;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Business.Services;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Controllers;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.IntegrationTests.Common;
using Rookie.AssetManagement.IntegrationTests.TestData;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Rookie.AssetManagement.IntegrationTests
{
    public class AssetControllerShould : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly BaseRepository<Asset> _assetRepository;
        private readonly BaseRepository<Category> _categoryRepository;
        private readonly BaseRepository<ReturnRequest> _returnRequestRepository;
        private readonly BaseRepository<Assignment> _assignmentRepository;
        private readonly AssetService _assetService;
        private readonly AssetController _assetController;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        public AssetControllerShould(SqliteInMemoryFixture fixture)
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
            _assetService = new AssetService(_userManager, _assetRepository, _categoryRepository, _assignmentRepository, _mapper);
            _assetController = new AssetController(_assetService);
            RoleArrangeData.InitRolesDataAsync(_roleManager).Wait();
            UserArrangeData.InitUsersDataAsync(_userManager).Wait();
            AssetArrangeData.InitAssetsDataAsync(_assetRepository).Wait();       
            AssignmentArrangeData.InitAssignmentDataAsync(_assignmentRepository).Wait();
            ReturnRequestArrangeData.InitReturnRequestDataAsync(_returnRequestRepository).Wait();
        }
        [Fact]
        public async Task CreateAssetSuccess()
        {
            //Arrange
            var assetCreateDto = AssetArrangeData.GetAssetCreateDto();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(UserClaims.Id, "1"),
                new Claim(UserClaims.StaffCode, "SD0000"),
                new Claim(UserClaims.FullName, "Tran Cuong"),
                new Claim(UserClaims.Location, "HCM"),
                new Claim(UserClaims.Role, "ADMIN")
            }, "mock"));
            _assetController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            ActionResult<AssetResponseDto> result;

            //Action
            using (var scope = _dbContext.Database.BeginTransaction())
            {
                result = (ActionResult)await _assetController.CreateAsset(assetCreateDto);
            }

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var createdAsset = Assert.IsType<AssetResponseDto>(actionResult.Value);
            Assert.Equal(assetCreateDto.AssetName, createdAsset.AssetName);
            Assert.Equal(assetCreateDto.CategoryId, createdAsset.CategoryId);
            Assert.Equal(assetCreateDto.InstalledDate, createdAsset.InstalledDate);
            Assert.Equal(assetCreateDto.Specification, createdAsset.Specification);
            Assert.Equal(assetCreateDto.State, createdAsset.State);
        }

        [Fact]
        public async Task EditAssetSuccess()
        {
            //Arrange
            var assetEditDto = AssetArrangeData.GetAssetEditDto();

            ActionResult<AssetResponseDto> result;

            //Action
            using (var scope = _dbContext.Database.BeginTransaction())
            {
                result = (ActionResult)await _assetController.EditAsset(assetEditDto);
                scope.Dispose();
            }

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var editedAsset = Assert.IsType<AssetResponseDto>(actionResult.Value);
            Assert.Equal(assetEditDto.Id, editedAsset.Id);
            Assert.Equal(assetEditDto.AssetName, editedAsset.AssetName);
            Assert.Equal(assetEditDto.InstalledDate, editedAsset.InstalledDate);
            Assert.Equal(assetEditDto.Specification, editedAsset.Specification);
            Assert.Equal(assetEditDto.State, editedAsset.State);
        }

        [Fact]
        public async Task GetAssetsSuccess()
        {
            //Arrage
            var assetQueryCriteria = AssetArrangeData.GetAssetQueryCriteraDto();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(UserClaims.Id, "1"),
                new Claim(UserClaims.StaffCode, "SD0000"),
                new Claim(UserClaims.FullName, "Tran Cuong"),
                new Claim(UserClaims.Location, "HCM"),
                new Claim(UserClaims.Role, "ADMIN")
            }, "mock"));
            _assetController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            //Action
            ActionResult<PagedResponseModel<AssetDto>> result =
                (ActionResult)await _assetController.GetAssetList(assetQueryCriteria, System.Threading.CancellationToken.None);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var pagedResponse = Assert.IsType<PagedResponseModel<AssetDto>>(actionResult.Value);
            Assert.Equal(3, pagedResponse.TotalItems);
            Assert.Equal(1, pagedResponse.TotalPages);
            pagedResponse.Items.Should()
                .NotBeEmpty()
                .And.HaveCount(3);           
        }

        [Fact]
        public async Task GetAssetSuccess()
        {
            //Arrange
            var id = 1;

            //Action
            ActionResult<AssetDetailDto> result = (ActionResult)await _assetController.GetAssetByIdAsync(id);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var assetDetailDto = Assert.IsType<AssetDetailDto>(actionResult.Value);
            Assert.NotNull(assetDetailDto);
        }

        [Fact]
        public async Task GetAssetHistorySuccess()
        {
            //Arrange
            var id = 1;

            //Action
            ActionResult<AssetHistoryDto> result = (ActionResult)await _assetController.GetListAssetHistoryAsync(id);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);

            var assetDetailDto = Assert.IsType<List<AssetHistoryDto>>(actionResult.Value);
            Assert.NotNull(assetDetailDto);
        }
    }
}