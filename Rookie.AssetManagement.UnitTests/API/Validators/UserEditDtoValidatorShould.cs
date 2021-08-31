using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using Rookie.AssetManagement.Tests.Validations;
using Rookie.AssetManagement.UnitTests.API.Validators.TestData;
using Rookie.AssetManagement.Validators;
using System;
using Xunit;

namespace Rookie.AssetManagement.UnitTests.API.Validators
{
    public class UserEditDtoValidatorShould : BaseValidatorShould
    {
        private readonly ValidationTestRunner<UserEditDtoValidator, UserEditDto> _testRunner;

        public UserEditDtoValidatorShould()
        {
            _testRunner = ValidationTestRunner
                .Create<UserEditDtoValidator, UserEditDto>(new UserEditDtoValidator());
        }

        [Theory]
        [MemberData(nameof(UserTestData.ValidDateOfBirth), MemberType = typeof(UserTestData))]
        public void NotHaveErrorWhenDateOfBirthIsValid(DateTime dateOfBirth) =>
            _testRunner
            .For(m => m.DateOfBirth = dateOfBirth)
            .ShouldNotHaveErrorsFor(m => m.DateOfBirth);

        [Theory]
        [MemberData(nameof(UserTestData.InvalidDateOfBirth), MemberType = typeof(UserTestData))]
        public void HaveErrorwhenDateOfBirthIsInvalid(DateTime dateOfBirth, string errorMessage) =>
            _testRunner
            .For(m => m.DateOfBirth = dateOfBirth)
            .ShouldHaveErrorsFor(m => m.DateOfBirth, errorMessage);

        [Theory]
        [MemberData(nameof(UserTestData.ValidJoinedDate), MemberType = typeof(UserTestData))]
        public void NotHaveErrorWhenJoinedDateIsValid(DateTime joinedDate) =>
            _testRunner
            .For(m => m.JoinedDate = joinedDate)
            .ShouldNotHaveErrorsFor(m => m.JoinedDate);

        [Theory]
        [MemberData(nameof(UserTestData.InvalidJoinedDate), MemberType = typeof(UserTestData))]
        public void HaveErrorWhenJoinedDateIsInvalid(DateTime joinedDate, string errorMessage) =>
            _testRunner
            .For(m => m.JoinedDate = joinedDate)
            .ShouldHaveErrorsFor(m => m.JoinedDate, errorMessage);

        [Theory]
        [MemberData(nameof(UserTestData.ValidEditJoinedDateAndDateOfBirth), MemberType = typeof(UserTestData))]
        public void NotHaveErrorWhenJoinedDateAndDateOfBirthIsValid(UserEditDto userEditDto) =>
            _testRunner
            .For(userEditDto)   
            .ShouldNotHaveErrorsFor(m => m);

        [Theory]
        [MemberData(nameof(UserTestData.InvalidEditJoinedDateAndDateOfBirth), MemberType = typeof(UserTestData))]
        public void HaveErrorWhenJoinedDateAndDateOfBirthIsInvalid(UserEditDto userEditDto, string errorMessage) =>
            _testRunner
            .For(userEditDto)
            .ShouldHaveErrorsFor(m => m, errorMessage);


        [Theory]
        [MemberData(nameof(UserTestData.ValidType), MemberType = typeof(UserTestData))]
        public void NotHaveErrorWhenTypeIsValid(string type) =>
            _testRunner
            .For(m => m.Type = type)
            .ShouldNotHaveErrorsFor(m => m.Type);

        [Theory]
        [MemberData(nameof(UserTestData.InvalidType), MemberType = typeof(UserTestData))]
        public void HaveErrorWhenTypeIsInvalid(string type, string errorMessage) =>
            _testRunner
            .For(m => m.Type = type)
            .ShouldHaveErrorsFor(m => m, errorMessage);
    }
}
