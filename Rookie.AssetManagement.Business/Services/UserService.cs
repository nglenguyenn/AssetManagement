using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.Contracts.Constants;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Text.RegularExpressions;
using Rookie.AssetManagement.Contracts.Enums;
using System.Threading;
using Rookie.AssetManagement.Contracts;
using System.Collections.Generic;

namespace Rookie.AssetManagement.Business.Services
{
    public class UserService : ControllerBase, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;
        public UserService(UserManager<User> userManager, IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> CreateUserAsync(ClaimsIdentity identity, [FromBody] UserCreateDto userCreateDto)
        {
            if (identity == null || identity.FindFirst(UserClaims.Role).Value != UserConstants.Common.AdminRole) return Unauthorized();
            if (userCreateDto == null) return BadRequest();

            Enum.TryParse(identity.FindFirst(UserClaims.Location).Value, out Location adminLocation);
            string lastestStaffCode = _userRepository.Entities.OrderByDescending(m => m.StaffCode).Select(m => m.StaffCode).First().Substring(2);
            StringBuilder newStaffCode = new StringBuilder();
            newStaffCode.Append(UserConstants.Common.StaffCodePrefix)
                        .Append((int.Parse(lastestStaffCode) + 1).ToString(UserConstants.Common.FourNumbersFormat));

            StringBuilder userName = new StringBuilder();
            StringBuilder regexString = new StringBuilder();
            string[] firstLettersLastName = userCreateDto.LastName.Split(" ");
            userName.Append(userCreateDto.FirstName);
            foreach (string firstLetter in firstLettersLastName)
            {
                userName.Append(firstLetter.Substring(0, 1));
            }
            regexString.Append(@"^").Append(userName.ToString().ToLower()).Append(@"\d*$");
            Regex regex = new Regex(regexString.ToString());
            var lastestDuplicateUserName = _userRepository.Entities.AsEnumerable()
                .Where(m => Regex.IsMatch(m.UserName, regexString.ToString()));
            if (lastestDuplicateUserName.Any())
            {
                var dupNumber = lastestDuplicateUserName
                .OrderByDescending(m => m.UserName)
                .Select(m => m.UserName)
                .First().Substring(userName.Length);
                var isDupNumberParsable = int.TryParse(dupNumber, out int numberAppened);
                if (!string.IsNullOrEmpty(dupNumber))
                    userName.Append(numberAppened + 1).ToString();
                else
                    userName.Append(1);
            }
            StringBuilder password = new StringBuilder();
            var localTime = userCreateDto.DateOfBirth;
            password.Append(userName)
                .Append("@")
                .Append(localTime.AddMinutes(-userCreateDto.TimeOffset).ToString(UserConstants.Common.DayMonthYearFormat));
            password = password.Replace("/", "");

            var newUser = new User();
            _mapper.Map(userCreateDto, newUser);
            newUser.StaffCode = newStaffCode.ToString();
            newUser.UserName = userName.ToString().ToLower();
            newUser.Location = adminLocation;
            await _userManager.CreateAsync(newUser, password.ToString().ToLower());
            await _userManager.AddToRoleAsync(newUser, newUser.Type);
            return Ok(newUser);
        }

        public async Task<IActionResult> EditUserAsync(ClaimsIdentity identity, [FromBody] UserEditDto userEditDto)
        {
            if (identity == null || identity.FindFirst(UserClaims.Role).Value != UserConstants.Common.AdminRole) return Unauthorized();
            if (userEditDto == null) return BadRequest();

            var user = await _userManager.FindByIdAsync(userEditDto.Id.ToString());
            if (user == null) return NotFound(ErrorTypes.User.UserNotFoundError);
            var oldRole = user.Type;

            _mapper.Map(userEditDto, user);
            if (!_userManager.GetRolesAsync(user).Result.Contains(userEditDto.Type))
            {
                await _userManager.RemoveFromRoleAsync(user, oldRole);
                await _userManager.AddToRoleAsync(user, userEditDto.Type);
            }
            await _userManager.UpdateAsync(user);
            return Ok(user);
        }
            public async Task<IActionResult> GetUserListAsync(
                ClaimsIdentity identity,
                UserQueryCriteriaDto userQueryCriteria,
                CancellationToken cancellationToken)
            {
                var id = identity.FindFirst(UserClaims.Id).Value;
                if (id == null)
                    return Unauthorized(ErrorTypes.User.TokenNotFoundError);
                var user = await _userManager.FindByIdAsync(id);
                if (user == null || user.Type != UserConstants.Common.AdminRole)
                    return Unauthorized();
                userQueryCriteria.Location = user.Location;
                var userQuery = UserFilter(
                    _userRepository.Entities.Where(u => !u.IsDisabled).AsQueryable(),
                    userQueryCriteria);
                var users = await userQuery.AsNoTracking().PaginateAsync(userQueryCriteria, cancellationToken);
                var userDtos = _mapper.Map<IEnumerable<UserDto>>(users.Items);

                return Ok(new PagedResponseModel<UserDto> {
                    CurrentPage = users.CurrentPage,
                    TotalPages = users.TotalPages,
                    TotalItems = users.TotalItems,
                    Items = userDtos
                });
            }

            private IQueryable<User> UserFilter(
                IQueryable<User> userQuery,
                UserQueryCriteriaDto userQueryCriteria)
            {
                var location = userQueryCriteria.Location;
                var searchValue = userQueryCriteria.Search;

                userQuery = userQuery.Where(u => u.Location == location);
                if (!String.IsNullOrEmpty(searchValue))
                {
                    userQuery = userQuery.Where(u => u.StaffCode.Contains(searchValue) ||
                    (u.FirstName + " " + u.LastName).Contains(searchValue));
                }
                if (userQueryCriteria.Types != null &&
                    userQueryCriteria.Types.Count() > 0 &&
                    !userQueryCriteria.Types.Any(x => x == UserTypeFilter.All))
                {
                    userQuery = userQuery.Where(u =>
                        userQueryCriteria.Types.Any(t => t == u.Type));
                }

                return userQuery;
            }

            public async Task<UserDto> GetUserByIdAsync(int id)
            {
                var user = await _userRepository.Entities
                    .Where(x => !x.IsDisabled)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (user == null)
                {
                    throw new NotFoundException($"User {id} is not found");
                }
                var userDto = _mapper.Map<UserDto>(user);

                return userDto;
            }
        }
    } 

