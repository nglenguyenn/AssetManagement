using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Business;
using Rookie.AssetManagement.Business.Services;
using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.IntegrationTests.Common;
using Rookie.AssetManagement.IntegrationTests.TestData;
using Rookie.AssetManagement.UnitTests.API.Validators.TestData;
using System.Threading.Tasks;
using Xunit;

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
                UserName = "adminhcm",
                Password = "123456"
            };

            //Act
            var result = await _accountService.LoginAsync(loginAccount);
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
                UserName = "falseName",
                Password = "falsePassword"
            };

            //Act
            var result = await _accountService.LoginAsync(loginAccount);
            var invalidLoginResult = result as ObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, invalidLoginResult.StatusCode);
        }
        [Fact]
        public async Task ChangePasswordFirstTimeShouldBeSuccess()
        {
            //Arrange
            var claimsidentity = AccountTestData.GetClaims();
            var changePassDto = new AccountChangePasswordFirstTimeDto { NewPassword = "1234567" };
            //Act
            var result = await _accountService.ChangePasswordFirstTimeAsync(claimsidentity, changePassDto);
            var objResult = result as ObjectResult;
            var user = _userManager.FindByIdAsync(claimsidentity.FindFirst(UserClaims.Id).Value);

            //Assert
            Assert.NotNull(result);
            Assert.Contains("Password Changed Successfully", objResult.Value.ToString());
            Assert.True(user.Result.IsFirstChangePassword);
        }
        [Fact]
        public async Task ChangePasswordFirstTimeWithSamePasswordShouldFail()
        {
            //Arrange
            var claimsidentity = AccountTestData.GetClaims();
            var changePassDto = new AccountChangePasswordFirstTimeDto { NewPassword = "123456" };
            //Act
            var result = await _accountService.ChangePasswordFirstTimeAsync(claimsidentity, changePassDto);
            var objResult = result as ObjectResult;
            var user = _userManager.FindByIdAsync(claimsidentity.FindFirst(UserClaims.Id).Value);

            //Assert
            Assert.NotNull(result);
            Assert.Contains(ErrorTypes.User.NewPasswordIsNotDifferent, objResult.Value.ToString());
            
        }

        [Fact]
        public async Task CheckIfChangedPasswordShouldBeSuccess()
        {
            //Arrange
            var claimsidentity = AccountTestData.GetClaims();

            //Act
            var result = await _accountService.CheckIfPasswordChangedAsync(claimsidentity);
            var okResult = result as ObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(okResult.Value);

        }

        [Fact]
        public async Task ChangePasswordShouldBeSuccess()
        {
            //Arrange
            var claimsidentity = AccountTestData.GetClaims();

            var changePassword = new AccountChangePasswordDto()
            {
                OldPassword = "123456",
                NewPassword = "test12345"
            };

            //Act
            var result = await _accountService.ChangePasswordAsync(claimsidentity, changePassword);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}