using AutoMapper;
//using FluentAssertions;
using Rookie.AssetManagement.Tests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Rookie.AssetManagement.Business;
using Rookie.AssetManagement.Business.Services;
using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using Rookie.AssetManagement.Controllers;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.IntegrationTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.IntegrationTests.TestData;
using Rookie.AssetManagement.DataAccessor.Data.Seeds;
using System.Security.Claims;
using Rookie.AssetManagement.Constants;
using Rookie.AssetManagement.Contracts.Constants;
using System.IdentityModel.Tokens.Jwt;

namespace Rookie.AssetManagement.IntegrationTests
{
    public class AccountControllerShould : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly BaseRepository<User> _userRepository;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        //private readonly IMapper _mapper;
        private readonly AccountService _accountService;
        private readonly AccountController _accountController;

        public AccountControllerShould(SqliteInMemoryFixture fixture)
        {
            fixture.CreateDatabase();
            _dbContext = fixture.Context;
            _userManager = fixture.UserManager;
            _roleManager = fixture.RoleManager;
            _userRepository = new BaseRepository<User>(_dbContext);
            _accountService = new AccountService(_userManager, _userRepository);
            _accountController = new AccountController(_accountService);
            RoleArrangeData.InitRolesDataAsync(_roleManager).Wait();
            UserArrangeData.InitUsersDataAsync(_userManager).Wait();
        }

        [Theory]
        [InlineData("adminhcm", "123456")]
        public async Task Login_As_Admin_Success(string username, string password)
        {
            //Arrange
            var loginAccount = new AccountLoginDto()
            {
                Username = username,
                Password = password
            };

            //Act
            var result = await _accountController.Login(loginAccount);
            var okResult = result as ObjectResult;
            var response = okResult.Value as AccountResponse;
            var role = GetRoleFromTokenExtension.GetRole(response);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<AccountResponse>(okResult.Value);
            Assert.Equal(role, Roles.Admin);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

        }

        [Theory]
        [InlineData("stafftest1", "123456")]
        public async Task Login_As_Staff_Success(string username, string password)
        {
            //Arrange
            var loginAccount = new AccountLoginDto()
            {
                Username = username,
                Password = password
            };

            //Act
            var result = await _accountController.Login(loginAccount);
            var okResult = result as ObjectResult;
            var response = okResult.Value as AccountResponse;
            var role = GetRoleFromTokenExtension.GetRole(response);


            //Assert
            Assert.NotNull(result);
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<AccountResponse>(okResult.Value);
            Assert.Equal(role, Roles.Staff);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

        }

        [Theory]
        [InlineData("654321")]
        public async Task Change_Password_First_Time_Success(string password)
        {
            //Arrange
            var newPassDto = new AccountChangePasswordFirstTimeDto()
            {
                NewPassword = password
            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(UserClaims.Id, "1"),
                new Claim(UserClaims.StaffCode, "SD0000"),
                new Claim(UserClaims.FullName, "Tran Cuong"),
                new Claim(UserClaims.Location, "HCM"),
                new Claim(UserClaims.Role, "ADMIN")
            }, "mock"));
            _accountController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            //var identity = _accountController.HttpContext.User.Identity as ClaimsIdentity;

            //Act
            var result = await _accountController.ChangePasswordFirstTimeLogin(newPassDto);
            var okResult = result as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
    }
}
