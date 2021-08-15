using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Interfaces
{
	public interface IAccountService
	{
		Task<IActionResult> Login([FromBody] AccountLoginDto accountLoginDto);
		Task<IActionResult> getAccountRoleAsync(ClaimsIdentity identity);
        Task<IActionResult> ChangePasswordFirstTime(ClaimsIdentity identity, [FromBody] AccountChangePasswordFirstTimeDto FirstResetDto);
		Task<IActionResult> CheckIfPasswordChanged(ClaimsIdentity identity);
        Task<IActionResult> ChangePassword(ClaimsIdentity identity, AccountChangePasswordDto accountChangePasswordDto);
		Task<IActionResult> GetAccountAsync(ClaimsIdentity identity, string token);
	}
}
