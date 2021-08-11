using AutoMapper.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.Contracts.Constants;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Services
{
	public class AccountService : ControllerBase, IAccountService
	{
        private readonly UserManager<User> userManager;

        public AccountService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> Login([FromBody] AccountLoginDto accountLoginDto)
		{
            if (!ModelState.IsValid) return NoContent();

            var user = await userManager.FindByNameAsync(accountLoginDto.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, accountLoginDto.Password))
            {

                if (user.IsDisabled) return Ok(new AccountResponse()
                {
                    Status = "Disabled",
                    Token = null
                });


                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(UserClaims.Id, user.Id.ToString()),
                    new Claim(UserClaims.StaffCode, user.StaffCode),
                    new Claim(UserClaims.FullName, $"{user.FirstName} {user.LastName}"),
                    new Claim(UserClaims.Location, user.Location.ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(UserClaims.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Key));

                var token = new JwtSecurityToken(
                    issuer: JWTSettings.Issuer,
                    audience: JWTSettings.Audience,
                    expires: DateTime.Now.AddMinutes(JWTSettings.DurationInMinutes),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );


                if (user.IsFirstChangePassword)
                {
                    return Ok(new AccountResponse()
                    {
                        Status = "NeedChangePassword",
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                    });
                }
                else return Ok(new AccountResponse()
                {
                    Status = "Success",
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                });

            }
            return Unauthorized();
        }
    }
}
