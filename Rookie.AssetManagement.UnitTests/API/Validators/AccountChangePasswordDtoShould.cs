using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using Rookie.AssetManagement.Tests.Validations;
using Rookie.AssetManagement.UnitTests.API.Validators.TestData;
using Rookie.AssetManagement.Validators;
using Xunit;

namespace Rookie.AssetManagement.UnitTests.API.Validators
{
    public class AccountChangePasswordDtoShould : BaseValidatorShould
    {
        private readonly ValidationTestRunner<AccountChangePasswordDtoValidator, AccountChangePasswordDto> _testRunner;

        public AccountChangePasswordDtoShould()
        {
            _testRunner = ValidationTestRunner
                .Create<AccountChangePasswordDtoValidator, AccountChangePasswordDto>(new AccountChangePasswordDtoValidator());
        }

        [Theory]
        [MemberData(nameof(AccountTestData.ValidPassword), MemberType = typeof(AccountTestData))]
        public void NotHaveErrorWhenOldPasswordIsValid(string oldpassword) =>
            _testRunner
                .For(m => m.OldPassword = oldpassword)
                .ShouldNotHaveErrorsFor(m => m.OldPassword);

        [Theory]
        [MemberData(nameof(AccountTestData.InValidOldPassword), MemberType = typeof(AccountTestData))]
        public void HaveErrorWhenOldPasswordIsInValid(string oldpassword, string errorMessage) =>
            _testRunner
                .For(m => m.OldPassword = oldpassword)
                .ShouldHaveErrorsFor(m => m.OldPassword, errorMessage);

        [Theory]
        [MemberData(nameof(AccountTestData.InValidNewPasswordChange), MemberType = typeof(AccountTestData))]
        public void HaveErrorWhenNewPasswordIsInValid(string password, string errorMessage) =>
            _testRunner
                .For(m => m.NewPassword = password)
                .ShouldHaveErrorsFor(m => m.NewPassword, errorMessage);
    }
}
