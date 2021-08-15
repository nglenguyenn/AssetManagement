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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Rookie.AssetManagement.Business.Services
{
	public class AccountService : ControllerBase, IAccountService
	{
        private readonly UserManager<User> userManager;
        private readonly IBaseRepository<User> _userRepository;

        public AccountService(UserManager<User> userManager, IBaseRepository<User> userRepository)
        {
            this.userManager = userManager;
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Login([FromBody] AccountLoginDto accountLoginDto)
		{
            var user = await userManager.FindByNameAsync(accountLoginDto.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, accountLoginDto.Password))
            {

                if (user.IsDisabled) return Unauthorized("Your account is disabled. Please contact with IT Team");


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


                string message = null;
                if (!user.IsFirstChangePassword)
                {
                    message = "NeedChangePassword";
                }
                else message = "Success";

                return Ok(new AccountResponse()
                {
                    Status = message,
                    Id = user.Id,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Username = user.UserName,
                    Role = userRoles?[0],
                    Fullname = $"{user.FirstName} {user.LastName}",
                    StaffCode = user.StaffCode,
                    Location = user.Location.ToString(),
                });

            }
            return Unauthorized("Username or password is incorrect. Please try again");
        }

        public async Task<IActionResult> getAccountRoleAsync(ClaimsIdentity identity)
        {
            var id = identity.FindFirst(UserClaims.Id)?.Value;
            if (id == null) return Unauthorized();

            var user = await userManager.FindByIdAsync(id);
            if (user.IsDisabled) return Unauthorized();

            var userRoles = await userManager.GetRolesAsync(user);
            return Ok(new { userRoles });
        }

        public async Task<IActionResult> ChangePasswordFirstTime(ClaimsIdentity identity, [FromBody] AccountChangePasswordFirstTimeDto accountChangePassFirstTimeDto)
        {

            var id = identity.FindFirst(UserClaims.Id).Value;

            var user = await userManager.FindByIdAsync(id);
            
            if (user.IsFirstChangePassword == false)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var result = await userManager.ResetPasswordAsync(user, token, accountChangePassFirstTimeDto.NewPassword);

                user.IsFirstChangePassword = true;

                await _userRepository.Update(user);

                return Ok("Password Changed Successfully");
            }
            else return BadRequest("Password Already Changed Once");
        }

        public async Task<IActionResult> CheckIfPasswordChanged(ClaimsIdentity identity)
        {
            var id = identity.FindFirst(UserClaims.Id).Value;

            var user = await userManager.FindByIdAsync(id);
            return Ok(user.IsFirstChangePassword);
        }

        public async Task<IActionResult> ChangePassword(ClaimsIdentity identity, [FromBody] AccountChangePasswordDto accountChangePasswordDto)
        {
            var id = identity.FindFirst(UserClaims.Id).Value;
            var user = await userManager.FindByIdAsync(id);

            if (await userManager.CheckPasswordAsync(user, accountChangePasswordDto.OldPassword))
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, token, accountChangePasswordDto.NewPassword);
                if (result.Succeeded)
                    return Ok();
                else
                    return StatusCode(500);
            }
            return Conflict("Password is incorrect");
        }
        public async Task<IActionResult> GetAccountAsync(ClaimsIdentity identity, string token)
		{
            var id = identity.FindFirst(UserClaims.Id)?.Value;
            if (id == null) 
                return Unauthorized("Token not found");

            var user = await userManager.FindByIdAsync(id);
            if (user == null) 
                return Unauthorized("User not found");

            if (user.IsDisabled) 
                return Unauthorized("Your account is disabled. Please contact with IT Team");

            var userRoles = await userManager.GetRolesAsync(user);

            var tokenElement = token.Split(' ');

            if (tokenElement.Length > 1)
            {
                token = tokenElement[1];
            }
            else return BadRequest();


            string message = null;
            if (!user.IsFirstChangePassword)
			{
                message = "NeedChangePassword";
            }
            else message = "Success";

            return Ok(new AccountResponse()
            {
                Status = message,
                Id = user.Id,
                Token = token,
                Username = user.UserName,
                Role = userRoles?[0],
                Fullname = $"{user.FirstName} {user.LastName}",
                StaffCode = user.StaffCode,
                Location = user.Location.ToString(),
            });
        }
    }
}
