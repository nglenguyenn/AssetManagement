using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using Rookie.AssetManagement.Tests.Validations;
using Rookie.AssetManagement.UnitTests.API.Validators.TestData;
using Rookie.AssetManagement.Validators;
using Xunit;

namespace Rookie.AssetManagement.UnitTests.API.Validators
{
    public class AccountChangePasswordDtoValidatorShould : BaseValidatorShould
    {
        private readonly ValidationTestRunner<AccountChangePasswordDtoValidator, AccountChangePasswordDto> _testRunner;
        public AccountChangePasswordDtoValidatorShould()
        {
            _testRunner = ValidationTestRunner
                .Create<AccountChangePasswordDtoValidator, AccountChangePasswordDto>(new AccountChangePasswordDtoValidator());
        }

        [Theory]
        [MemberData(nameof(AccountTestData.ValidPassword), MemberType = typeof(AccountTestData))]
        public void NotHaveErrorWhenOldPasswordIsValid(string oldPassword) =>
            _testRunner
                .For(m => m.OldPassword = oldPassword)
                .ShouldNotHaveErrorsFor(m => m.OldPassword);

        [Theory]
        [MemberData(nameof(AccountTestData.InValidOldPassword), MemberType = typeof(AccountTestData))]
        public void HaveErrorWhenOldPasswordIsInValid(string password, string errorMessage) =>
            _testRunner
                .For(m => m.OldPassword = password)
                .ShouldHaveErrorsFor(m => m.OldPassword, errorMessage);

        [Theory]
        [MemberData(nameof(AccountTestData.InValidNewPasswordChange), MemberType = typeof(AccountTestData))]
        public void HaveErrorWhenNewPasswordIsInValid(string password, string errorMessage) =>
            _testRunner
                .For(m => m.NewPassword = password)
                .ShouldHaveErrorsFor(m => m.NewPassword, errorMessage);
    }
}
