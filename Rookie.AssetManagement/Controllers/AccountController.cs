using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using System.Security.Claims;
using System.Threading.Tasks;


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
        public async Task<IActionResult> Login([FromBody] AccountLoginDto accountLoginDto)
        {
            return await _accountService.LoginAsync(accountLoginDto);
        }

        [HttpPost]
        [Route("changePasswordFirstTimeLogin")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordFirstTimeLogin([FromBody] AccountChangePasswordFirstTimeDto accountChangePasswordFirstTimeDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return await _accountService.ChangePasswordFirstTimeAsync(identity, accountChangePasswordFirstTimeDto);
        }
        [Authorize]
        [HttpGet]
        [Route("checkIfPasswordChanged")]
        public async Task<IActionResult> CheckIfPasswordChanged()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return await _accountService.CheckIfPasswordChangedAsync(identity);
        }

        [Authorize]
        [HttpPost]
        [Route("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] AccountChangePasswordDto accountChangePasswordDto)
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
        public async Task<IActionResult> GetAccount()
        {
            var token = Request.Headers[HeaderNames.Authorization];
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return await _accountService.GetAccountAsync(identity, token);
        }
    }
}
