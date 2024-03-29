﻿using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Rookie.AssetManagement.IntegrationTests
{
    public static class GetRoleFromTokenExtension
    {
        public static string GetRole(AccountResponse response)
        {
            var token = response.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            var role = jsonToken.Claims.First(u => u.Type == UserClaims.Role).Value;

            return role;
        }
    }
}
