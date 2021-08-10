using System;
using System.Threading.Tasks;
using Rookie.AssetManagement.Constants;
using Microsoft.AspNetCore.Identity;
using Rookie.AssetManagement.DataAccessor.Entities;
using System.Linq;
using System.Collections.Generic;

namespace Rookie.AssetManagement.DataAccessor.Data.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedAsync(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var adminHCM = new User
                {
                    UserName = "adminhcm",
                    StaffCode = "SD0000",
                    JoinedDate = DateTime.Now,
                    DateOfBirth = DateTime.Now.AddYears(-18),
                    Location = Location.HCM,
                    Type = Roles.Admin,
                    IsDisabled = false,
                    IsFirstChangePassword = false, 
                };

                var adminHN = new User
                {
                    UserName = "adminhn",
                    StaffCode = "SD0001",
                    JoinedDate = DateTime.Now,
                    DateOfBirth = DateTime.Now.AddYears(-18),
                    Location = Location.HN,
                    Type = Roles.Admin,
                    IsDisabled = false,
                    IsFirstChangePassword = false,
                };

                IdentityResult resultHCM =  await userManager.CreateAsync(adminHCM, AdminPassword.AdminHCM);
                IdentityResult resultHN = await userManager.CreateAsync(adminHN, AdminPassword.AdminHN); 

                if(resultHCM.Succeeded)
                {
                    userManager.AddToRoleAsync(adminHCM, Roles.Admin).Wait();
                }
                if (resultHN.Succeeded)
                {
                    userManager.AddToRoleAsync(adminHN, Roles.Admin).Wait();
                }
            }

        }
    }
}