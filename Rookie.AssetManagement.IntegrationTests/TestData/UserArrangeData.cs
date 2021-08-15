using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Rookie.AssetManagement.Constants;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;

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
    }
}