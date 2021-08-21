using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Business;
using Rookie.AssetManagement.Business.Services;
using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Controllers;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.IntegrationTests.Common;
using Rookie.AssetManagement.IntegrationTests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rookie.AssetManagement.IntegrationTests
{
    public class AssetControllerShould : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly BaseRepository<Asset> _assetRepository;
        private readonly BaseRepository<Category> _categoryRepository;
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
            _assetService = new AssetService(_userManager, _assetRepository, _categoryRepository, _mapper);
            _assetController = new AssetController(_assetService);
            RoleArrangeData.InitRolesDataAsync(_roleManager).Wait();
            UserArrangeData.InitUsersDataAsync(_userManager).Wait();
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
                result = (ActionResult) await _assetController.CreateAsset(assetCreateDto);
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
    }
}
