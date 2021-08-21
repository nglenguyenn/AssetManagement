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
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using MockQueryable.Moq;
using Rookie.AssetManagement.UnitTests.API.Validators.TestData;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Contracts.Constants;
using System.Security.Claims;

namespace Rookie.AssetManagement.UnitTests.Business
{
    public class AssetServiceShould
    {
        private readonly AssetService _assetService;
        private readonly Mock<IBaseRepository<Asset>> _assetRepository;
        private readonly Mock<IBaseRepository<Category>> _categoryRepository;
        private readonly UserManager<User> _userManager;
        private readonly Mock<IUserStore<User>> _userStore;
        private readonly Mock<IPasswordHasher<User>> _passwordHasher;

        private readonly IMapper _mapper;

        public AssetServiceShould()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            _assetRepository = new Mock<IBaseRepository<Asset>>();
            _categoryRepository = new Mock<IBaseRepository<Category>>();
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
            _assetService = new AssetService(_userManager, _assetRepository.Object, _categoryRepository.Object, _mapper);
        }

        [Fact]
        public async Task ValidAssetCreateDtoWithSameCateExistedAssetShouldBeSuccess()
        {
            //Arrange
            var user = UserTestData.GetClaims();
            var assetCreateDto = AssetTestData.GetAssetCreateDto(2);

            var assets = new List<Asset>();
            assets.Add(AssetTestData.GetAsset(1, "MO000001", 2));
            _assetRepository
                .Setup(x => x.Entities)
                .Returns(assets.AsQueryable().BuildMock().Object);

            var categories = AssetTestData.GetCategories().AsQueryable().BuildMock();
            _categoryRepository
                .Setup(x => x.Entities)
                .Returns(categories.Object);
            //Action
            IActionResult result = await _assetService.CreateAssetAsync(user, assetCreateDto);

            //Assert
            result.Should().NotBeNull();
            _assetRepository.Verify(mock => mock.Add(It.IsAny<Asset>()), Times.Once);
        }

        [Fact]
        public async Task ValidAssetCreateDtoWithoutSameCateExistedAssetShouldBeSuccess()
        {
            //Arrange
            var user = UserTestData.GetClaims();
            var assetCreateDto = AssetTestData.GetAssetCreateDto(1);

            var assets = new List<Asset>();
            _assetRepository
                .Setup(x => x.Entities)
                .Returns(assets.AsQueryable().BuildMock().Object);

            var categories = AssetTestData.GetCategories().AsQueryable().BuildMock();
            _categoryRepository
                .Setup(x => x.Entities)
                .Returns(categories.Object);
            //Action
            IActionResult result = await _assetService.CreateAssetAsync(user, assetCreateDto);

            //Assert
            result.Should().NotBeNull();
            _assetRepository.Verify(mock => mock.Add(It.IsAny<Asset>()), Times.Once);
        }

        [Fact]
        public async Task NullAssetCreateDtoShouldBeBadRequest()
        {
            //Arrange
            var user = UserTestData.GetClaims();

            //Action
            IActionResult result = await _assetService.CreateAssetAsync(user, null);

            //Assert
            result.Should().NotBeNull();
            var badRequestResult = new BadRequestResult();
            Assert.Equal(badRequestResult.ToString(), result.ToString());
        }

        [Fact]
        public async Task NotAdminInIdentityShouldBeUnauthorized()
        {
            //Arrange
            var assetCreateDto = AssetTestData.GetAssetCreateDto(1);
            var user = UserTestData.GetClaims();
            user.RemoveClaim(user.FindFirst(UserClaims.Role));
            user.AddClaim(new Claim(UserClaims.Role, "STAFF"));

            //Action
            IActionResult result = await _assetService.CreateAssetAsync(user, assetCreateDto);

            //Assert
            result.Should().NotBeNull();
            var unauthorizedResult = new UnauthorizedResult();
            Assert.Equal(unauthorizedResult.ToString(), result.ToString());
        }
    }
}
