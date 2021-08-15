using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Interfaces
{
      public interface IAccountService
      {
            Task<IActionResult> LoginAsync([FromBody] AccountLoginDto accountLoginDto);
            Task<IActionResult> getAccountRoleAsync(ClaimsIdentity identity);
            Task<IActionResult> ChangePasswordFirstTimeAsync(ClaimsIdentity identity, [FromBody] AccountChangePasswordFirstTimeDto FirstResetDto);
            Task<IActionResult> CheckIfPasswordChangedAsync(ClaimsIdentity identity);
            Task<IActionResult> ChangePasswordAsync(ClaimsIdentity identity, AccountChangePasswordDto accountChangePasswordDto);
            Task<IActionResult> GetAccountAsync(ClaimsIdentity identity, string token);
      }
}
