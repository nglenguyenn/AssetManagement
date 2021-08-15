using AutoMapper;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using Rookie.AssetManagement.Business;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Business.Services;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.IntegrationTests.Common;
using Microsoft.AspNetCore.Identity;
using Rookie.AssetManagement.UnitTests.API.Validators.TestData;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Rookie.AssetManagement.IntegrationTests.TestData;
using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Rookie.AssetManagement.Contracts.Constants;

namespace Rookie.AssetManagement.UnitTests.Business
{
        public class AccountServiceShould : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly AccountService _accountService;
        private readonly BaseRepository<User> _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMapper _mapper;
        public AccountServiceShould(SqliteInMemoryFixture fixture)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();
            

            fixture.CreateDatabase();
            _dbContext = fixture.Context;
            _userManager = fixture.UserManager;
            _roleManager = fixture.RoleManager;
            _userRepository = new BaseRepository<User>(_dbContext);

            _accountService = new AccountService(_userManager, _userRepository);

            RoleArrangeData.InitRolesDataAsync(_roleManager).Wait();
            UserArrangeData.InitUsersDataAsync(_userManager).Wait();
        }

        [Fact]        
        public async Task ValidLoginShouldBeSuccess()
        {
            //Arrange
            var loginAccount = new AccountLoginDto()
            {
                Username = "adminhcm",
                Password = "123456"
            };
            
            //Act
            var result = await _accountService.Login(loginAccount);
            var okResult = result as ObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
        [Fact]
        public async Task InvalidLoginShouldFail()
        {
            //Arrange
            var loginAccount = new AccountLoginDto()
            {
                Username = "falseName",
                Password = "falsePassword"
            };

            //Act
            var result = await _accountService.Login(loginAccount);
            var invalidLoginResult = result as ObjectResult; 
            //Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, invalidLoginResult.StatusCode);
        }
        [Fact]
        public async Task GetAccountRoleShouldBeSuccess()
        {
            //Arrange
            var claimsidentity = AccountTestData.GetClaims();

            //Act
            var result = await _accountService.getAccountRoleAsync(claimsidentity);
            //Assert
            Assert.NotNull(result);
            
        }
        [Fact]
        public async Task ChangePasswordFirstTimeShouldBeSuccess()
        {
            //Arrange
            var claimsidentity = AccountTestData.GetClaims();
            var changePassDto = new AccountChangePasswordFirstTimeDto { NewPassword = "123456" };
            //Act
            var result = await _accountService.ChangePasswordFirstTime(claimsidentity,changePassDto);
            var objResult = result as ObjectResult;
            var user = _userManager.FindByIdAsync(claimsidentity.FindFirst(UserClaims.Id).Value);
            
            //Assert
            Assert.NotNull(result);
            Assert.Contains("Password Changed Successfully", objResult.Value.ToString());
            Assert.True(user.Result.IsFirstChangePassword);
        }

        [Fact]
        public async Task CheckIfChangedPasswordShouldBeSuccess()
        {
            //Arrange
            var claimsidentity = AccountTestData.GetClaims();
            
            //Act
            var result = await _accountService.CheckIfPasswordChanged(claimsidentity);
            var okResult = result as ObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(okResult.Value);

        }
    }
}