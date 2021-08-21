using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using Rookie.AssetManagement.Tests.Validations;
using Rookie.AssetManagement.UnitTests.API.Validators.TestData;
using Rookie.AssetManagement.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rookie.AssetManagement.UnitTests.API.Validators
{
    public class UserCreateDtoValidatorShould : BaseValidatorShould
    {
        private readonly ValidationTestRunner<UserCreateDtoValidator, UserCreateDto> _testRunner;

        public UserCreateDtoValidatorShould()
        {
            _testRunner = ValidationTestRunner
                .Create<UserCreateDtoValidator, UserCreateDto>(new UserCreateDtoValidator());
        }

        [Theory]
        [MemberData(nameof(UserTestData.ValidFirstName), MemberType = typeof(UserTestData))]
        public void NotHaveErrorWhenFirstNameIsValid(string firstName) =>
            _testRunner
            .For(m => m.FirstName = firstName)
            .ShouldNotHaveErrorsFor(m => m.FirstName);

        [Theory]
        [MemberData(nameof(UserTestData.InvalidFirstName), MemberType = typeof(UserTestData))]
        public void HaveErrorWhenFirstNameIsInvalid(string firstName, string errorMessage) =>
            _testRunner
            .For(m => m.FirstName = firstName)
            .ShouldHaveErrorsFor(m => m.FirstName, errorMessage);


        [Theory]
        [MemberData(nameof(UserTestData.ValidLastName), MemberType = typeof(UserTestData))]
        public void NotHaveErrorWhenLastNameIsValid(string lastName) =>
            _testRunner
            .For(m => m.LastName = lastName)
            .ShouldNotHaveErrorsFor(m => m.LastName);

        [Theory]
        [MemberData(nameof(UserTestData.InvalidLastName), MemberType = typeof(UserTestData))]
        public void HaveErrorWhenLastNameIsInvalid(string lastName, string errorMessage) =>
            _testRunner
            .For(m => m.FirstName = lastName)
            .ShouldHaveErrorsFor(m => m.LastName, errorMessage);

        [Theory]
        [MemberData(nameof(UserTestData.ValidDateOfBirth), MemberType = typeof(UserTestData))]
        public void NotHaveErrorWhenDateOfBirthIsValid(DateTime dateOfBirth) =>
            _testRunner
            .For(m => m.DateOfBirth = dateOfBirth)
            .ShouldNotHaveErrorsFor(m => m.DateOfBirth);

        [Theory]
        [MemberData(nameof(UserTestData.InvalidDateOfBirth), MemberType = typeof(UserTestData))]
        public void HaveErrorWhenDateOfBirthIsInvalid(DateTime dateOfBirth, string errorMessage) =>
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

        [Theory]
        [MemberData(nameof(UserTestData.ValidJoinedDateAndDateOfBirth), MemberType = typeof(UserTestData))]
        public void NotHaveErrorWhenJoinedDateAndDateOfBirthIsValid(UserCreateDto userCreateDto) =>
            _testRunner
            .For(userCreateDto)
            .ShouldNotHaveErrorsFor(m => m);

        [Theory]
        [MemberData(nameof(UserTestData.InvalidJoinedDateAndDateOfBirth), MemberType = typeof(UserTestData))]
        public void HaveErrorWhenJoinedDateAndDateOfBirthIsInvalid(UserCreateDto userCreateDto, string errorMessage) =>
            _testRunner
            .For(userCreateDto)
            .ShouldHaveErrorsFor(m => m, errorMessage);


    }
}
