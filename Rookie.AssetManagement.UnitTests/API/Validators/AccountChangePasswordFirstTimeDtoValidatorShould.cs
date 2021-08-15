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
    public class AccountChangePasswordFirstTimeDtoValidatorShould : BaseValidatorShould
    {
        private readonly ValidationTestRunner<AccountChangePasswordFirstTimeDtoValidator, AccountChangePasswordFirstTimeDto> _testRunner;

        public AccountChangePasswordFirstTimeDtoValidatorShould()
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
