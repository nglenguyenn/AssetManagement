using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return await _userService.CreateUserAsync(identity, userCreateDto);
        }

        [Authorize]
        [HttpGet]
        [Route("getList")]
        public async Task<IActionResult> GetUsers([FromQuery] UserQueryCriteriaDto userQueryCriteria, CancellationToken cancellationToken)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return await _userService.GetUserListAsync(identity, userQueryCriteria, cancellationToken);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userResponses = await _userService.GetUserByIdAsync(id);
            return Ok(userResponses);
        }

        [Authorize]
        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> EditUser([FromBody] UserEditDto userEditDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return await _userService.EditUserAsync(identity, userEditDto);
        }
    }
}
