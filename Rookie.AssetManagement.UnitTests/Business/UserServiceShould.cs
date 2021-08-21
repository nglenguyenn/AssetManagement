using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Rookie.AssetManagement.Business;
using Rookie.AssetManagement.Business.Services;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.IntegrationTests.Common;
using Rookie.AssetManagement.IntegrationTests.TestData;
using Rookie.AssetManagement.UnitTests.API.Validators.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;
using MockQueryable.Moq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Enums;

namespace Rookie.AssetManagement.UnitTests.Business
{
    public class UserServiceShould
    {
        private readonly UserService _userService;
        private readonly Mock<IBaseRepository<User>> _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly Mock<IUserStore<User>> _userStore;
        private readonly Mock<IPasswordHasher<User>> _passwordHasher;

        private readonly IMapper _mapper;

        public UserServiceShould()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            _userRepository = new Mock<IBaseRepository<User>>();
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
            _userService = new UserService(_userManager, _userRepository.Object, _mapper);
        }

        [Fact]
        public async Task ValidUserShouldBeSuccess()
        {
            //Arrage
            var userId = 3;
            var existedUsers = UserTestData.GetUsers().AsQueryable().BuildMock();

            var newUser = UserTestData.GetUser(userId, "SD0003");
            var user = UserTestData.GetClaims();
            var userCreateDto = UserTestData.GetUserCreateDto();

            _userStore
                .Setup(x => x.CreateAsync(newUser, CancellationToken.None))
                .ReturnsAsync(IdentityResult.Success);
            _userRepository
                .Setup(x => x.Entities)
                .Returns(existedUsers.Object);
            //Action
            IActionResult result = await _userService.CreateUserAsync(user, userCreateDto);

            //Assert
            result.Should().NotBeNull();
            _userStore.Verify(mock => mock.CreateAsync(It.IsAny<User>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task NullIdentityShouldBeUnauthorized()
        {
            //Arrage
            var userCreateDto = UserTestData.GetUserCreateDto();

            //Action
            IActionResult result = await _userService.CreateUserAsync(null, userCreateDto);

            //Assert
            var unauthorizedResult = new UnauthorizedResult();
            Assert.Equal(unauthorizedResult.ToString(), result.ToString());
        }

        [Fact]
        public async Task NotRoleAdminShouldBeUnauthorized()
        {
            //Arrage
            var userCreateDto = UserTestData.GetUserCreateDto();
            var user = UserTestData.GetClaims();
            user.RemoveClaim(user.FindFirst(UserClaims.Role));
            user.AddClaim(new Claim(UserClaims.Role, "STAFF"));

            //Action
            IActionResult result = await _userService.CreateUserAsync(user, userCreateDto);

            //Assert
            var unauthorizedResult = new UnauthorizedResult();
            Assert.Equal(unauthorizedResult.ToString(), result.ToString());
        }

        [Fact]
        public async Task NullUserCreateDtoShouldBeUnauthorized()
        {
            //Arrage
            var user = UserTestData.GetClaims();

            //Action
            IActionResult result = await _userService.CreateUserAsync(user, null);

            //Assert
            var badRequestResult = new BadRequestResult();
            Assert.Equal(badRequestResult.ToString(), result.ToString());
        }

        [Fact]
        public async Task ValidUserWithOneDuplicateUsernameShouldBeSuccess()
        {
            //Arrage
            var userId = 3;
            var existeduser = UserTestData.GetUser(4, "SD0004");
            var listUsers = UserTestData.GetUsers();
            listUsers.Add(existeduser);
            var existedUsers = listUsers.AsQueryable().BuildMock();

            var newUser = UserTestData.GetUser(userId, "SD0003");
            var user = UserTestData.GetClaims();
            var userCreateDto = UserTestData.GetUserCreateDto();

            _userStore
                .Setup(x => x.CreateAsync(newUser, CancellationToken.None))
                .ReturnsAsync(IdentityResult.Success);
            _userRepository
                .Setup(x => x.Entities)
                .Returns(existedUsers.Object);
            //Action
            IActionResult result = await _userService.CreateUserAsync(user, userCreateDto);

            //Assert
            result.Should().NotBeNull();
            _userStore.Verify(mock => mock.CreateAsync(It.IsAny<User>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task ValidUserWithMoreOneDuplicateUsernameShouldBeSuccess()
        {
            //Arrage
            var userId = 3;
            var existeduser = UserTestData.GetUser(4, "SD0004");
            var listUsers = UserTestData.GetUsers();
            listUsers.Add(existeduser);
            existeduser.UserName = $"{existeduser.UserName}1";
            listUsers.Add(existeduser);
            var existedUsers = listUsers.AsQueryable().BuildMock();

            var newUser = UserTestData.GetUser(userId, "SD0003");
            var user = UserTestData.GetClaims();
            var userCreateDto = UserTestData.GetUserCreateDto();

            _userStore
                .Setup(x => x.CreateAsync(newUser, CancellationToken.None))
                .ReturnsAsync(IdentityResult.Success);
            _userRepository
                .Setup(x => x.Entities)
                .Returns(existedUsers.Object);
            //Action
            IActionResult result = await _userService.CreateUserAsync(user, userCreateDto);

            //Assert
            result.Should().NotBeNull();
            _userStore.Verify(mock => mock.CreateAsync(It.IsAny<User>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task ValidUserQueryCriteriaShouldBeSuccess()
        {
            //Arrage
            var existedUsers = UserTestData.GetUsers().AsQueryable().BuildMock();
            var identity = UserTestData.GetClaims();
            var userQueryCriteria = UserTestData.GetUserQueryCriteriaDto();
            var idIdentity = identity.FindFirst(UserClaims.Id).Value;
            var userIdentity = UserTestData.GetUserFromClaims();

            _userStore
                .Setup(x => x.FindByIdAsync(idIdentity, CancellationToken.None))
                .ReturnsAsync(userIdentity);
            _userRepository
                .Setup(x => x.Entities)
                .Returns(existedUsers.Object);
            //Action
            ActionResult<PagedResponseModel<UserDto>> result = 
                (ActionResult) await _userService.GetUserListAsync(identity, userQueryCriteria, CancellationToken.None);

            //Assert
            _userRepository.Verify(mock => mock.Entities, Times.Once);
            result.Should().NotBeNull();
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var pagedResponse = Assert.IsType<PagedResponseModel<UserDto>>(actionResult.Value);

            Assert.Equal(2, pagedResponse.TotalItems);
            Assert.Equal(1, pagedResponse.TotalPages);
            pagedResponse.Items.Should()
                .NotBeEmpty()
                .And.HaveCount(2);
            pagedResponse.Items.Should().OnlyContain(x => x.Location == Location.HCM);
        }

        [Fact]
        public async Task ValidUserQueryCriteriaWithSearchShouldBeSuccess()
        {
            //Arrage
            var existedUsers = UserTestData.GetUsers().AsQueryable().BuildMock();
            var identity = UserTestData.GetClaims();
            var userQueryCriteria = UserTestData.GetUserQueryCriteriaDto();
            userQueryCriteria.Search = "SD0001";
            var idIdentity = identity.FindFirst(UserClaims.Id).Value;
            var userIdentity = UserTestData.GetUserFromClaims();

            _userStore
                .Setup(x => x.FindByIdAsync(idIdentity, CancellationToken.None))
                .ReturnsAsync(userIdentity);
            _userRepository
                .Setup(x => x.Entities)
                .Returns(existedUsers.Object);
            //Action
            ActionResult<PagedResponseModel<UserDto>> result =
                (ActionResult) await _userService.GetUserListAsync(identity, userQueryCriteria, CancellationToken.None);

            //Assert
            _userRepository.Verify(mock => mock.Entities, Times.Once);
            result.Should().NotBeNull();
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var pagedResponse = Assert.IsType<PagedResponseModel<UserDto>>(actionResult.Value);

            Assert.Equal(1, pagedResponse.TotalItems);
            Assert.Equal(1, pagedResponse.TotalPages);
            pagedResponse.Items.Should()
                .NotBeEmpty()
                .And.HaveCount(1);
            pagedResponse.Items.Should()
                .OnlyContain(x => x.Location == Location.HCM && x.StaffCode.Contains(userQueryCriteria.Search));
        }

        [Fact]
        public async Task ValidUserQueryCriteriaWithTypeShouldBeSuccess()
        {
            //Arrage
            var existedUsers = UserTestData.GetUsers().AsQueryable().BuildMock();
            var identity = UserTestData.GetClaims();
            var userQueryCriteria = UserTestData.GetUserQueryCriteriaDto();
            userQueryCriteria.Types = new string[] { UserTypeFilter.Admin };
            var idIdentity = identity.FindFirst(UserClaims.Id).Value;
            var userIdentity = UserTestData.GetUserFromClaims();

            _userStore
                .Setup(x => x.FindByIdAsync(idIdentity, CancellationToken.None))
                .ReturnsAsync(userIdentity);
            _userRepository
                .Setup(x => x.Entities)
                .Returns(existedUsers.Object);
            //Action
            ActionResult<PagedResponseModel<UserDto>> result =
                (ActionResult)await _userService.GetUserListAsync(identity, userQueryCriteria, CancellationToken.None);

            //Assert
            _userRepository.Verify(mock => mock.Entities, Times.Once);
            result.Should().NotBeNull();
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var pagedResponse = Assert.IsType<PagedResponseModel<UserDto>>(actionResult.Value);

            Assert.Equal(0, pagedResponse.TotalItems);
            Assert.Equal(0, pagedResponse.TotalPages);
            pagedResponse.Items.Should().BeEmpty();
        }

        [Fact]
        public async Task NullIdentityWhenGetUsersShouldBeUnauthorized()
        {
            //Arrage
            var existedUsers = UserTestData.GetUsers().AsQueryable().BuildMock();
            var identity = UserTestData.GetClaims();
            var userQueryCriteria = UserTestData.GetUserQueryCriteriaDto();
            userQueryCriteria.Types = new string[] { UserTypeFilter.Admin };

            _userRepository
                .Setup(x => x.Entities)
                .Returns(existedUsers.Object);
            //Action
            ActionResult<PagedResponseModel<UserDto>> result =
                (ActionResult)await _userService.GetUserListAsync(identity, userQueryCriteria, CancellationToken.None);

            //Assert
            _userRepository.Verify(mock => mock.Entities, Times.Never);
            result.Should().NotBeNull();
            var actionResult = Assert.IsType<UnauthorizedResult>(result.Result);
        }

        [Fact]
        public async Task ValidUserIdShouldBeSuccess()
        {
            //Arrange
            var id = 1;
            var existedUsers = UserTestData.GetUsers().AsQueryable().BuildMock();
            var resultUser = existedUsers.Object.Where(x => x.Id == id).FirstOrDefault();
            _userRepository
                .Setup(x => x.Entities)
                .Returns(existedUsers.Object);

            //Action
            var result = await _userService.GetUserByIdAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(resultUser.StaffCode, result.StaffCode);
            Assert.Equal(resultUser.UserName, result.UserName);
            Assert.Equal(resultUser.DateOfBirth, result.DateOfBirth);
            Assert.Equal(resultUser.FirstName, result.FirstName);
            Assert.Equal(resultUser.LastName, result.LastName);
            Assert.Equal(resultUser.Location, result.Location);
            Assert.Equal(resultUser.Type, result.Type);
            Assert.Equal(resultUser.JoinedDate, result.JoinedDate);
            Assert.Equal(resultUser.Gender, result.Gender);
        }

        [Fact]
        public async Task InvalidUserIdShoudThrowException()
        {
            //Arrange
            var id = -1;
            var existedUsers = UserTestData.GetUsers().AsQueryable().BuildMock();
            _userRepository
                .Setup(x => x.Entities)
                .Returns(existedUsers.Object);

            //Action
            Func<Task> result = () => _userService.GetUserByIdAsync(id);

            //Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(result);
            Assert.Contains($"User {id} is not found", exception.Message);
        }

    }
}
