using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Contracts.Constants
{
    public static class UserTypeFilter
    {
        public const string All = "ALL";
        public const string Admin = UserConstants.Common.AdminRole;
        public const string Staff = UserConstants.Common.StaffRole;
    }
}
