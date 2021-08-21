using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using System.Collections.Generic;
using System.Security.Claims;

namespace Rookie.AssetManagement.UnitTests.API.Validators.TestData
{
    public static class AccountTestData
    {
        public static IEnumerable<object[]> ValidUsername()
        {
            return new object[][]
            {
                new object[] { "test1" },
                new object[] { "test2" },
            };
        }

        public static IEnumerable<object[]> InValidUsername()
        {
            return new object[][]
            {
                new object[]
                {
                    null,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(AccountLoginDto.UserName)),
                }
            };
        }

        public static IEnumerable<object[]> ValidPassword()
        {
            return new object[][]
            {
                new object[] { "test123" },
                new object[] { "test456" },
            };
        }

        public static IEnumerable<object[]> InValidPassword()
        {
            return new object[][]
            {
                new object[]
                {
                    null,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(AccountLoginDto.Password)),
                }
            };
        }

        public static IEnumerable<object[]> InValidNewPassword()
        {
            return new object[][]
            {
                new object[]
                {
                    null,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(AccountChangePasswordFirstTimeDto.NewPassword)),
                }
            };
        }
        public static ClaimsIdentity GetClaims()
        {
            var authClaims = new List<Claim>
                {
                    new Claim(UserClaims.Id, "1"),
                    new Claim(UserClaims.StaffCode, "SD0001"),
                    new Claim(UserClaims.FullName, $"Tester Full Name"),
                    new Claim(UserClaims.Location, "HN"),
                };
            var claimsIdentity = new ClaimsIdentity(authClaims);
            return claimsIdentity;
        }

        public static IEnumerable<object[]> InValidNewPasswordChange()
        {
            return new object[][]
            {
                new object[]
                {
                    null,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(AccountChangePasswordDto.NewPassword)),
                }
            };
        }

        public static IEnumerable<object[]> InValidOldPassword()
        {
            return new object[][]
            {
                new object[]
                {
                    null,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(AccountChangePasswordDto.OldPassword)),
                }
            };
        }
    }
}
