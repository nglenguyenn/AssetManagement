using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Business;
using Rookie.AssetManagement.Business.Services;
using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Controllers;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.IntegrationTests.Common;
using Rookie.AssetManagement.IntegrationTests.TestData;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using Rookie.AssetManagement.Contracts.Enums;

namespace Rookie.AssetManagement.IntegrationTests
{
    public class UserControllerShould : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly BaseRepository<User> _userRepository;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserService _userService;
        private readonly UserController _userController;

        private readonly IMapper _mapper;

        public UserControllerShould(SqliteInMemoryFixture fixture)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            fixture.CreateDatabase();
            _dbContext = fixture.Context;
            _userManager = fixture.UserManager;
            _roleManager = fixture.RoleManager;
            _userRepository = new BaseRepository<User>(_dbContext);
            _userService = new UserService(_userManager, _userRepository, _mapper);
            _userController = new UserController(_userService);
            RoleArrangeData.InitRolesDataAsync(_roleManager).Wait();
            UserArrangeData.InitUsersDataAsync(_userManager).Wait();
        }

        [Fact]
        public async Task CreateUserSuccess()
        {
            //Arrange
            var userCreateDto = UserArrangeData.GetUserCreateDto();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(UserClaims.Id, "1"),
                new Claim(UserClaims.StaffCode, "SD0000"),
                new Claim(UserClaims.FullName, "Tran Cuong"),
                new Claim(UserClaims.Location, "HCM"),
                new Claim(UserClaims.Role, "ADMIN")
            }, "mock"));
            _userController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            ActionResult<User> result;

            //Action
            using (var scope = _dbContext.Database.BeginTransaction())
            {
                result = (ActionResult)await _userController.CreateUser(userCreateDto);
                scope.Dispose();
            }

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var createdUser = Assert.IsType<User>(actionResult.Value);
            Assert.Equal(userCreateDto.FirstName, createdUser.FirstName);
            Assert.Equal(userCreateDto.LastName, createdUser.LastName);
            Assert.Equal(userCreateDto.DateOfBirth, createdUser.DateOfBirth);
            Assert.Equal(userCreateDto.Gender, createdUser.Gender);
            Assert.Equal(userCreateDto.JoinedDate, createdUser.JoinedDate);
            Assert.Equal(userCreateDto.Type, createdUser.Type);
            Assert.Equal("SD0002", createdUser.StaffCode);
            Assert.Equal("anhtt", createdUser.UserName);
        }

        [Fact]
        public async Task GetUsersSuccess()
        {
            //Arrange
            var userQueryCriteria = UserArrangeData.GetUserQueryCriterDto();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(UserClaims.Id, "1"),
                new Claim(UserClaims.StaffCode, "SD0000"),
                new Claim(UserClaims.FullName, "Tran Cuong"),
                new Claim(UserClaims.Location, "HCM"),
                new Claim(UserClaims.Role, "ADMIN")
            }, "mock"));
            _userController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            //Action
            ActionResult<PagedResponseModel<UserDto>> result = 
                (ActionResult) await _userController.GetUsers(userQueryCriteria, CancellationToken.None);

            //Assert
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
        public async Task GetUSerSuccess()
        {
            //Arrange
            var id = 1;

            //Action
            ActionResult<UserDto> result = (ActionResult)await _userController.GetUserById(id);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var userDto = Assert.IsType<UserDto>(actionResult.Value);
            Assert.NotNull(userDto);
        }
    }
}
