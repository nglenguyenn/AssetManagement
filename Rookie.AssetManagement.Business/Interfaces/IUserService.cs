using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;


namespace Rookie.AssetManagement.Business.Interfaces
{
    public interface IUserService
    {
        Task<IActionResult> CreateUserAsync(ClaimsIdentity identity, [FromBody] UserCreateDto userCreateDto);
        Task<IActionResult> EditUserAsync(ClaimsIdentity identity, [FromBody] UserEditDto userEditDto);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<IActionResult> GetUserListAsync(
            ClaimsIdentity identity,
            UserQueryCriteriaDto userQueryCriteria,
            CancellationToken cancellationToken);
    }
}
