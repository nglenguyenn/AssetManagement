using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using Rookie.AssetManagement.Tests.Validations;
using Rookie.AssetManagement.UnitTests.API.Validators.TestData;
using Rookie.AssetManagement.Validators;
using Xunit;

namespace Rookie.AssetManagement.UnitTests.API.Validators
{
    public class AccountChangePasswordFirstTimeDtoShould : BaseValidatorShould
    {
        private readonly ValidationTestRunner<AccountChangePasswordFirstTimeDtoValidator, AccountChangePasswordFirstTimeDto> _testRunner;

        public AccountChangePasswordFirstTimeDtoShould()
        {
            _testRunner = ValidationTestRunner
                .Create<AccountChangePasswordFirstTimeDtoValidator, AccountChangePasswordFirstTimeDto>(new AccountChangePasswordFirstTimeDtoValidator());
        }

        [Theory]
        [MemberData(nameof(AccountTestData.ValidPassword), MemberType = typeof(AccountTestData))]
        public void NotHaveErrorWhenPasswordIsValid(string password) =>
            _testRunner
                .For(m => m.NewPassword = password)
                .ShouldNotHaveErrorsFor(m => m.NewPassword);

        [Theory]
        [MemberData(nameof(AccountTestData.InValidNewPassword), MemberType = typeof(AccountTestData))]
        public void HaveErrorWhenPasswordIsInValid(string password, string errorMessage) =>
            _testRunner
                .For(m => m.NewPassword = password)
                .ShouldHaveErrorsFor(m => m.NewPassword, errorMessage);
    }
}
