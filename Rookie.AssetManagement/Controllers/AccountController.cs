using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace Rookie.AssetManagement.Controllers
{
      [ApiController]
      [Route("api/[controller]")]
      public class AccountController : ControllerBase
      {
            private readonly IAccountService _accountService;
            public AccountController(IAccountService accountService)
            {
                  _accountService = accountService;
            }

            [HttpPost]
            [Route("login")]
            public async Task<IActionResult> LoginAsync([FromBody] AccountLoginDto accountLoginDto)
            {
                  return await _accountService.LoginAsync(accountLoginDto);
            }

            [HttpGet]
            [Route("getAccountRole")]
            [Authorize]
            public async Task<IActionResult> getAccountRoleAsync()
            {
                  var identity = HttpContext.User.Identity as ClaimsIdentity;
                  return await _accountService.getAccountRoleAsync(identity);
            }

            [HttpPost]
            [Route("changePasswordFirstTimeLogin")]
            [Authorize]
            public async Task<IActionResult> ChangePasswordFirstTimeLoginAsync([FromBody] AccountChangePasswordFirstTimeDto accountChangePasswordFirstTimeDto)
            {
                  var identity = HttpContext.User.Identity as ClaimsIdentity;
                  return await _accountService.ChangePasswordFirstTimeAsync(identity, accountChangePasswordFirstTimeDto);
            }

            [Authorize]
            [HttpGet]
            [Route("checkIfPasswordChanged")]
            public async Task<IActionResult> CheckIfPasswordChangedAsync()
            {
                  var identity = HttpContext.User.Identity
                        as ClaimsIdentity;
                  return await _accountService.CheckIfPasswordChangedAsync(identity);
            }

            [Authorize]
            [HttpPost]
            [Route("changePassword")]
            public async Task<IActionResult> ChangePasswordAsync([FromBody] AccountChangePasswordDto accountChangePasswordDto)
            {
                  if (ModelState.IsValid)
                  {
                        var identity = HttpContext.User.Identity as ClaimsIdentity;
                        return await _accountService.ChangePasswordAsync(identity, accountChangePasswordDto);
                  }
                  return BadRequest();
            }

            [Authorize]
            [HttpGet]
            [Route("getAccount")]
            public async Task<IActionResult> GetAccountAsync()
            {
                  var token = Request.Headers[HeaderNames.Authorization];
                  var identity = HttpContext.User.Identity as ClaimsIdentity;
                  return await _accountService.GetAccountAsync(identity, token);
            }
      }
}
