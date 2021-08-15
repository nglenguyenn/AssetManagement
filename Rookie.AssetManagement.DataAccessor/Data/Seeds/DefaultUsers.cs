using System;
using System.Threading.Tasks;
using Rookie.AssetManagement.Constants;
using Microsoft.AspNetCore.Identity;
using Rookie.AssetManagement.DataAccessor.Entities;
using System.Collections.Generic;
using Rookie.AssetManagement.Contracts.Enums;
using Rookie.AssetManagement.Contracts.Constants;

namespace Rookie.AssetManagement.DataAccessor.Data.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedAsync(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("adminhn").Result == null ||userManager.FindByNameAsync("adminhcm").Result == null)
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

                foreach(User user in adminList)
                {
                    await AddAdminUser(userManager, user); 
                }

            }

        }

        public static async Task AddAdminUser (UserManager<User> userManager, User user)
        {
            string adminPassword = "";

            switch(user.Location)
            {
                case Location.HCM:
                    adminPassword = AdminPassword.AdminHCM;
                    break;
                case Location.HN:
                    adminPassword = AdminPassword.AdminHN;
                    break;
                default:
                    return; 
            }


            IdentityResult result = await userManager.CreateAsync(user, adminPassword);

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, Roles.Admin).Wait();
            }
        }
    }
}