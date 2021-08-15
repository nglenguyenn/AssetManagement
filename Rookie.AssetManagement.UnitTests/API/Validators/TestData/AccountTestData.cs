using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
