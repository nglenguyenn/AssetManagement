﻿using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
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
    public class AccountLoginDtoValidatorShould : BaseValidatorShould
    {
        private readonly ValidationTestRunner<AccountLoginDtoValidator, AccountLoginDto> _testRunner;

        public AccountLoginDtoValidatorShould()
        {
            _testRunner = ValidationTestRunner
                .Create<AccountLoginDtoValidator, AccountLoginDto>(new AccountLoginDtoValidator());
        }

        [Theory]
        [MemberData(nameof(AccountTestData.ValidUsername), MemberType = typeof(AccountTestData))]
        public void NotHaveErrorWhenUsernameIsValid(string username) =>
            _testRunner
                .For(m => m.Username = username)
                .ShouldNotHaveErrorsFor(m => m.Username);

        [Theory]
        [MemberData(nameof(AccountTestData.InValidUsername), MemberType = typeof(AccountTestData))]
        public void HaveErrorWhenUsernameIsInValid(string username, string errorMessage) =>
            _testRunner
                .For(m => m.Username = username)
                .ShouldHaveErrorsFor(m => m.Username, errorMessage);

        [Theory]
        [MemberData(nameof(AccountTestData.ValidPassword), MemberType = typeof(AccountTestData))]
        public void NotHaveErrorWhenPasswordIsValid(string password) =>
            _testRunner
                .For(m => m.Password = password)
                .ShouldNotHaveErrorsFor(m => m.Password);

        [Theory]
        [MemberData(nameof(AccountTestData.InValidPassword), MemberType = typeof(AccountTestData))]
        public void HaveErrorWhenPasswordIsInValid(string password, string errorMessage) =>
            _testRunner
                .For(m => m.Password = password)
                .ShouldHaveErrorsFor(m => m.Password, errorMessage);
    }
}