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
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Contracts.Enums;

namespace Rookie.AssetManagement.UnitTests.Business
{
    public class AssetServiceShould
    {
        private readonly AssetService _assetService;
        private readonly Mock<IBaseRepository<Asset>> _assetRepository;
        private readonly Mock<IBaseRepository<Category>> _categoryRepository;
        private readonly Mock<IBaseRepository<ReturnRequest>> _returnRequestRepository;
        private readonly Mock<IBaseRepository<Assignment>> _assignmentRepository;
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
            _assetService = new AssetService(
                _userManager,
                _assetRepository.Object,
                _categoryRepository.Object,
                _assignmentRepository.Object,
                _mapper);
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

        [Fact]
        public async Task ValidAssetEditDtoWithExistedIdShouldBeSuccess()
        {
            //Arrange
            int assetId = 1;
            var assetEditDto = AssetTestData.GetAssetEditDto(assetId);

            var assets = new List<Asset>();
            assets.Add(AssetTestData.GetAsset(assetId, "MO000001", 2));
            assets.Add(AssetTestData.GetAsset(assetId + 1, "MO000002", 2));
            _assetRepository
                .Setup(x => x.Entities)
                .Returns(assets.AsQueryable().BuildMock().Object);

            _assetRepository
                .Setup(x => x.GetById(assetId))
                .ReturnsAsync(assets.AsQueryable().BuildMock().Object.Where(a => a.Id == assetId).FirstOrDefault());

            //Action
            IActionResult result = await _assetService.EditAssetAsync(assetEditDto);

            //Assert
            result.Should().NotBeNull();
            _assetRepository.Verify(mock => mock.Update(It.IsAny<Asset>()), Times.Once);
        }

        [Fact]
        public async Task ValidAssetEditDtoWithInexistIdShouldBeNotFound()
        {
            //Arrange
            int assetId = 1;
            var assetEditDto = AssetTestData.GetAssetEditDto(assetId + 2);

            var assets = new List<Asset>();
            assets.Add(AssetTestData.GetAsset(assetId, "MO000001", 2));
            assets.Add(AssetTestData.GetAsset(assetId + 1, "MO000002", 2));
            _assetRepository
                .Setup(x => x.Entities)
                .Returns(assets.AsQueryable().BuildMock().Object);

            _assetRepository
                .Setup(x => x.GetById(assetId))
                .ReturnsAsync(assets.AsQueryable().BuildMock().Object.Where(a => a.Id == assetId).FirstOrDefault());

            //Action
            IActionResult result = await _assetService.EditAssetAsync(assetEditDto);

            //Assert
            result.Should().NotBeNull();
            Assert.Equal((new NotFoundObjectResult(ErrorTypes.Asset.AssetNotFoundError)).ToString(), result.ToString());
            _assetRepository.Verify(mock => mock.Update(It.IsAny<Asset>()), Times.Never);
        }

        [Fact]
        public async Task ValidAssetQueryCriteriaShouldBeSuccess()
        {//Arrange
            var assets = new List<Asset>
            {
                AssetTestData.GetAsset(1, "MO000001", 1),
                AssetTestData.GetAsset(2, "LA000001", 2),
                AssetTestData.GetAsset(3, "PC000001", 3),
            };
            _userStore.Setup(x => x.FindByIdAsync("1", CancellationToken.None)).ReturnsAsync(new User { Id = 1, Type = "ADMIN" });
            _assetRepository
               .Setup(x => x.Entities)
               .Returns(assets.AsQueryable().BuildMock().Object);
            var identity = UserTestData.GetClaims();
            var assetQueryCriteria = AssetTestData.GetAssetQueryCriteriaDto();
            var categories = AssetTestData.GetCategories().AsQueryable().BuildMock();
            _categoryRepository
                .Setup(x => x.Entities)
                .Returns(categories.Object);

            //Action
            ActionResult<PagedResponseModel<AssetDto>> result =
                (ActionResult)await _assetService.GetAssetListAsync(identity, assetQueryCriteria, CancellationToken.None);
            //Assert
            result.Should().NotBeNull();
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var pagedResponse = Assert.IsType<PagedResponseModel<AssetDto>>(actionResult.Value);
            Assert.Equal(3, pagedResponse.TotalItems);
            Assert.Equal(1, pagedResponse.TotalPages);
            pagedResponse.Items.Should()
                .NotBeEmpty()
                .And.HaveCount(3);
            pagedResponse.Items.Should().OnlyContain(x => x.Location == "HCM");
        }

        [Fact]
        public async Task ValidAssetQueryCriteriaWithStateAndSearchShouldBeSuccess()
        {//Arrange
            var assets = new List<Asset>
            {
                AssetTestData.GetAsset(1, "MO000001", 1),
                AssetTestData.GetAsset(2, "LA000001", 2),
                AssetTestData.GetAsset(3, "PC000001", 3),
            };
            _userStore.Setup(x => x.FindByIdAsync("1", CancellationToken.None)).ReturnsAsync(new User { Id = 1, Type = "ADMIN" });
            _assetRepository
               .Setup(x => x.Entities)
               .Returns(assets.AsQueryable().BuildMock().Object);
            var identity = UserTestData.GetClaims();
            var assetQueryCriteria = AssetTestData.GetAssetQueryCriteriaDto();
            assetQueryCriteria.State = new AssetState[] { AssetState.Available };
            assetQueryCriteria.Search = "MO000001";
            var categories = AssetTestData.GetCategories().AsQueryable().BuildMock();
            _categoryRepository
                .Setup(x => x.Entities)
                .Returns(categories.Object);

            //Action
            ActionResult<PagedResponseModel<AssetDto>> result =
                (ActionResult)await _assetService.GetAssetListAsync(identity, assetQueryCriteria, CancellationToken.None);
            //Assert
            result.Should().NotBeNull();
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var pagedResponse = Assert.IsType<PagedResponseModel<AssetDto>>(actionResult.Value);
            Assert.Equal(1, pagedResponse.TotalItems);
            Assert.Equal(1, pagedResponse.TotalPages);
            pagedResponse.Items.Should()
                .NotBeEmpty()
                .And.HaveCount(1);
            pagedResponse.Items.Should().OnlyContain(x => x.Location == "HCM"
            && x.AssetCode.Contains(assetQueryCriteria.Search)
            && assetQueryCriteria.State.Contains(x.State));
        }

        [Fact]
        public async Task ValidAssetIdShouldBeSuccess()
        {
            //Arrange
            int id = 1;
            var existedAssets = AssetTestData.GetAssets().AsQueryable().BuildMock();
            var resultAsset = existedAssets.Object.Where(x => x.Id == id).FirstOrDefault();
            _assetRepository
                .Setup(x => x.Entities)
                .Returns(existedAssets.Object);

            //Action
            var result = await _assetService.GetAssetByIdAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(resultAsset.AssetName, result.AssetName);
            Assert.Equal(resultAsset.AssetCode, result.AssetCode);
            Assert.Equal(resultAsset.Specification, result.Specification);
            Assert.Equal(resultAsset.InstalledDate, result.InstalledDate);
            Assert.Equal(resultAsset.Location, result.Location);
            Assert.Equal(resultAsset.CategoryId, result.CategoryId);
        }

        [Fact]
        public async Task InvalidAssetIdShoudThrowException()
        {
            //Arrange
            var id = -1;
            var existedAssets = AssetTestData.GetAssets().AsQueryable().BuildMock();
            _assetRepository
                .Setup(x => x.Entities)
                .Returns(existedAssets.Object);

            //Action
            Func<Task> result = () => _assetService.GetAssetByIdAsync(id);

            //Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(result);
            Assert.Contains($"Asset {id} is not found", exception.Message);
        }

        [Fact]
        public async Task ValidAssetHistoryShouldBeSuccess()
        {
            //Arrange
            int id = 1;

            var existedAsset = AssetTestData.GetAssets().AsQueryable().BuildMock();
            _assetRepository
                .Setup(x => x.Entities)
                .Returns(existedAsset.Object);

            var existedAssignment = AssignmentTestData.GetAssignments().AsQueryable().BuildMock();
            var resultAssignment = existedAssignment.Object
                .Where(x => x.AssetId == id && x.State == AssignmentState.Completed)
                .OrderByDescending(assetHistoryNewest => assetHistoryNewest.AssignedDate)
                .Take(5)
                .ToList();
            _assignmentRepository
                .Setup(x => x.Entities)
                .Returns(existedAssignment.Object);

            //Action
            var result = await _assetService.GetListAssetHistoryAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(resultAssignment.Count, result.ToList().Count);
        }

        [Fact]
        public async Task InvalidAssetHistoryShoudThrowException()
        {
            //Arrange
            var id = -1;

            var existedAsset = AssetTestData.GetAssets().AsQueryable().BuildMock();
            _assetRepository
                .Setup(x => x.Entities)
                .Returns(existedAsset.Object);

            var existedAssignment = AssignmentTestData.GetAssignments().AsQueryable().BuildMock();
            var resultAssignment = existedAssignment.Object
                .Where(x => x.AssetId == id && x.State == AssignmentState.Completed)
                .ToList();
            _assignmentRepository
                .Setup(x => x.Entities)
                .Returns(existedAssignment.Object);

            //Action
            Func<Task> result = () =>  _assetService.GetListAssetHistoryAsync(id);

            //Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(result);
            Assert.Contains($"Asset {id} is not found", exception.Message);
        }
    }
}
