using Microsoft.AspNetCore.Identity;
using Rookie.AssetManagement.Constants;
using Rookie.AssetManagement.Contracts.Enums;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.EnumDtos;
using System.Threading;

namespace Rookie.AssetManagement.IntegrationTests.TestData
{
    public static class UserArrangeData
    {
        public static async Task InitUsersDataAsync(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("adminhn").Result == null || userManager.FindByNameAsync("adminhcm").Result == null)
            {
                var adminList = new List<User>()
                {
                    new User
                    {
                        Id = 1,
                        UserName = "adminhcm",
                        StaffCode = "SD0000",
                        JoinedDate = DateTime.Now,
                        DateOfBirth = DateTime.Now.AddYears(-18),
                        Location = Location.HCM,
                        Type = Roles.Admin,
                        IsDisabled = false,
                        IsFirstChangePassword = false,
                    },
                    new User
                    {
                        Id = 2,
                        UserName = "adminhn",
                        StaffCode = "SD0001",
                        JoinedDate = DateTime.Now,
                        DateOfBirth = DateTime.Now.AddYears(-18),
                        Location = Location.HN,
                        Type = Roles.Admin,
                        IsDisabled = false,
                        IsFirstChangePassword = false,
                    },
                };

                foreach (User user in adminList)
                {
                    await AddAdminUser(userManager, user);
                }

            }

            if (userManager.FindByNameAsync("stafftest1").Result == null || userManager.FindByNameAsync("stafftest2").Result == null)
            {
                var staffList = new List<User>()
                {
                    new User
                    {
                        Id = 3,
                        UserName = "stafftest1",
                        StaffCode = "SD0000",
                        JoinedDate = DateTime.Now,
                        DateOfBirth = DateTime.Now.AddYears(-18),
                        Location = Location.HCM,
                        Type = Roles.Staff,
                        IsDisabled = false,
                        IsFirstChangePassword = false,
                    },
                    new User
                    {
                        Id = 4,
                        UserName = "stafftest2",
                        StaffCode = "SD0001",
                        JoinedDate = DateTime.Now,
                        DateOfBirth = DateTime.Now.AddYears(-18),
                        Location = Location.HN,
                        Type = Roles.Staff,
                        IsDisabled = false,
                        IsFirstChangePassword = false,
                    },
                };

                foreach (User user in staffList)
                {
                    await AddStaffUser(userManager, user);
                }

            }
        }

        public static async Task AddAdminUser(UserManager<User> userManager, User user)
        {

            IdentityResult result = await userManager.CreateAsync(user, "123456");

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, Roles.Admin).Wait();
            }
        }

        public static async Task AddStaffUser(UserManager<User> userManager, User user)
        {
            IdentityResult result = await userManager.CreateAsync(user, "123456");

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, Roles.Staff).Wait();
            }
        }

        public static UserCreateDto GetUserCreateDto()
        {
            return new UserCreateDto()
            {
                FirstName = "Anh",
                LastName = "Tran Tien",
                DateOfBirth = new DateTime(1996, 1, 1),
                Gender = true,
                JoinedDate = new DateTime(2015, 1, 1),
                Type = UserConstants.Common.StaffRole
            };
        }

        public static UserQueryCriteriaDto GetUserQueryCriterDto()
        {
            return new UserQueryCriteriaDto()
            {
                Limit = 5,
                Location = Location.HCM,
                Page = 1,
                SortColumn = "Id",
                SortOrder = SortOrderEnumDto.Decsending
            };
        }
    }
}
