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

            if (userManager.FindByNameAsync("NormalStaffHN").Result == null
                && userManager.FindByNameAsync("NormalStaffHCM").Result == null
                && userManager.FindByNameAsync("DisabledStaffHN").Result == null
                && userManager.FindByNameAsync("DisabledStaffHCM").Result == null)
            {
                var userList = new List<User>()
                {
                    new User
                    {
                        UserName = "NormalStaffHN",
                        StaffCode = "SD0002",
                        FirstName = "Staff",
                        LastName = "Normal",
                        DateOfBirth = DateTime.Now.AddYears(-18),
                        JoinedDate = DateTime.Now,
                        Location = Location.HN,
                        Type = Roles.Staff,
                        IsDisabled = false,
                        IsFirstChangePassword = false,
                    },
                    new User
                    {
                        UserName = "NormalStaffHCM",
                        StaffCode = "SD0003",
                        FirstName = "Staff",
                        LastName = "Normal",
                        DateOfBirth = DateTime.Now.AddYears(-18),
                        JoinedDate = DateTime.Now,
                        Location = Location.HCM,
                        Type = Roles.Staff,
                        IsDisabled = false,
                        IsFirstChangePassword = false,
                    },
                    new User
                    {
                        UserName = "DisabledStaffHN",
                        StaffCode = "SD0004",
                        FirstName = "Staff",
                        LastName = "Disable",
                        DateOfBirth = DateTime.Now.AddYears(-18),
                        JoinedDate = DateTime.Now,
                        Location = Location.HN,
                        Type = Roles.Staff,
                        IsDisabled = true,
                        IsFirstChangePassword = false,
                    },
                    new User
                    {
                        UserName = "DisabledStaffHCM",
                        StaffCode = "SD0005",
                        FirstName = "Staff",
                        LastName = "Disable",
                        DateOfBirth = DateTime.Now.AddYears(-18),
                        JoinedDate = DateTime.Now,
                        Location = Location.HCM,
                        Type = Roles.Staff,
                        IsDisabled = true,
                        IsFirstChangePassword = false,
                    },
                };

                foreach (User user in userList)
                {
                    await AddStaffUser(userManager, user);
                }
            }

        }

        public static async Task AddStaffUser(UserManager<User> userManager, User user)
        {
            string userPassword = "123456";

            IdentityResult result = await userManager.CreateAsync(user, userPassword);

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, Roles.Staff).Wait();
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