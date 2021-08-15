using Microsoft.AspNetCore.Identity;
using Rookie.AssetManagement.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.IntegrationTests.TestData
{
    public static class RoleArrangeData
    {
        public static async Task InitRolesDataAsync(RoleManager<IdentityRole<int>> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole<int>(Roles.Staff.ToString()));
            await roleManager.CreateAsync(new IdentityRole<int>(Roles.Admin.ToString()));
        }
    }
}
