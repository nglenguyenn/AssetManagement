using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using Rookie.AssetManagement.Contracts.EnumDtos;
using Rookie.AssetManagement.Contracts.Enums;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.UnitTests.API.Validators.TestData
{
    public class UserTestData
    {
        public static IEnumerable<object[]> ValidFirstName()
        {
            return new object[][]
            {
                new object[] { "firstname1" },
                new object[] { "firstname2" },
            };
        }

        public static IEnumerable<object[]> InvalidFirstName()
        {
            return new object[][]
            {
                new object[]
                {
                    null,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(UserCreateDto.FirstName))
                }
            };
        }

        public static IEnumerable<object[]> ValidLastName()
        {
            return new object[][]
            {
                new object[] { "lastname1" },
                new object[] { "lastname2" },
            };
        }

        public static IEnumerable<object[]> InvalidLastName()
        {
            return new object[][]
            {
                new object[]
                {
                    null,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(UserCreateDto.LastName))
                }
            };
        }

        public static IEnumerable<object[]> ValidDateOfBirth()
        {
            return new object[][]
            {
                new object[] { new DateTime(1996, 3, 27) },
                new object[] { new DateTime(1995, 10, 17) },
            };
        }

        public static IEnumerable<object[]> InvalidDateOfBirth()
        {
            return new object[][]
            {
                new object[]
                {
                    null,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(UserCreateDto.DateOfBirth))
                },
                new object[]
                {
                    new DateTime(2021, 8, 1),
                    string.Format(ErrorTypes.User.DateOfBirthError, nameof(UserCreateDto.DateOfBirth))
                }
            };
        }

        public static IEnumerable<object[]> ValidJoinedDate()
        {
            return new object[][]
            {
                new object[] { new DateTime(2015, 3, 27) },
                new object[] { new DateTime(2019, 10, 17) },
            };
        }

        public static IEnumerable<object[]> InvalidJoinedDate()
        {
            return new object[][]
            {
                new object[]
                {
                    null,
                    string.Format(ErrorTypes.Common.RequiredError, nameof(UserCreateDto.JoinedDate))
                }
            };
        }

        public static IEnumerable<object[]> ValidType()
        {
            return new object[][]
            {
                new object[] { UserConstants.Common.AdminRole },
                new object[] { UserConstants.Common.StaffRole },
            };
        }

        public static IEnumerable<object[]> InvalidType()
        {
            return new object[][]
            {
                new object[] {
                    "Manager",
                    string.Format(ErrorTypes.User.UserTypeError, nameof(UserCreateDto.Type))
                }
            };
        }

        public static IEnumerable<object[]> ValidJoinedDateAndDateOfBirth()
        {
            return new object[][]
            {
                new object[] {
                    new UserCreateDto() {
                        FirstName = "fisrt name",
                        LastName = " last name",
                        DateOfBirth = new DateTime(1996, 1, 1),
                        Gender = true,
                        JoinedDate = new DateTime(2015, 1, 1),
                        Type = UserConstants.Common.StaffRole
                    }
                }
            };
        }

        public static IEnumerable<object[]> ValidEditJoinedDateAndDateOfBirth()
        {
            return new object[][]
            {
                new object[]
                {
                    new UserEditDto()
                    {
                        DateOfBirth = new DateTime(1996, 1, 1),
                        Gender = true,
                        JoinedDate = new DateTime(2015, 1, 1),
                        Type = UserConstants.Common.StaffRole
                    }
                }
            };
        }

        public static IEnumerable<object[]> InvalidJoinedDateAndDateOfBirth()
        {
            return new object[][]
            {
                new object[] {
                    new UserCreateDto() {
                        FirstName = "fisrt name",
                        LastName = " last name",
                        DateOfBirth = new DateTime(1996, 1, 1),
                        Gender = true,
                        JoinedDate = new DateTime(1995, 1, 1),
                        Type = UserConstants.Common.StaffRole
                    },
                    string.Format(ErrorTypes.User.JoinedDateError, nameof(UserCreateDto))
                },
                new object[]
                {
                    new UserCreateDto()
                    {
                        FirstName = "fisrt name",
                        LastName = " last name",
                        DateOfBirth = new DateTime(1996, 1, 1),
                        Gender = true,
                        JoinedDate = new DateTime(2021, 8, 7),
                        Type = UserConstants.Common.StaffRole
                    },
                    string.Format(ErrorTypes.User.JoinedDateWeekendError, nameof(UserCreateDto))
                },
                new object[]
                {
                    new UserCreateDto()
                    {
                        FirstName = "fisrt name",
                        LastName = " last name",
                        DateOfBirth = new DateTime(1996, 1, 1),
                        Gender = true,
                        JoinedDate = new DateTime(2021, 8, 8),
                        Type = UserConstants.Common.StaffRole
                    },
                    string.Format(ErrorTypes.User.JoinedDateWeekendError, nameof(UserCreateDto))
                }
            };
        }

        public static IEnumerable<object[]> InvalidEditJoinedDateAndDateOfBirth()
        {
            return new object[][]
            {
                new object[]
                {
                    new UserEditDto
                    {
                        DateOfBirth = new DateTime(1996, 1, 1),
                        Gender = true,
                        JoinedDate = new DateTime(1995, 1, 1),
                        Type = UserConstants.Common.StaffRole
                    },
                    string.Format(ErrorTypes.User.JoinedDateError, nameof(UserCreateDto))
                },
                new object[]
                {
                    new UserEditDto()
                    {
                        DateOfBirth = new DateTime(1996, 1, 1),
                        Gender = true,
                        JoinedDate = new DateTime(2021, 8, 7),
                        Type = UserConstants.Common.StaffRole
                    },
                    string.Format(ErrorTypes.User.JoinedDateWeekendError, nameof(UserCreateDto))
                },
                new object[]
                {
                    new UserEditDto()
                    {
                        DateOfBirth = new DateTime(1996, 1, 1),
                        Gender = true,
                        JoinedDate = new DateTime(2021, 8, 8),
                        Type = UserConstants.Common.StaffRole
                    },
                    string.Format(ErrorTypes.User.JoinedDateWeekendError, nameof(UserCreateDto))
                }
            };
        }

        public static IList<string> GetRole(string type)
        {
            return new List<string>
            {
                "ADMIN"
            };
        }

        public static ClaimsIdentity GetClaims()
        {
            return new ClaimsIdentity(new Claim[]
            {
                new Claim(UserClaims.Id, "1"),
                new Claim(UserClaims.StaffCode, "SD0000"),
                new Claim(UserClaims.FullName, "Tran Cuong"),
                new Claim(UserClaims.Location, "HCM"),
                new Claim(UserClaims.Role, "ADMIN")
            }, "mock");
        }

        public static UserCreateDto GetUserCreateDto()
        {
            return new UserCreateDto()
            {
                FirstName = "An",
                LastName = "Nguyen",
                DateOfBirth = new DateTime(1996, 10, 10),
                Gender = true,
                JoinedDate = new DateTime(2019, 1, 1),
                Type = UserConstants.Common.StaffRole
            };
        }

        public static UserEditDto getUserEditDto()
        {
            return new UserEditDto()
            {
                Id = 1,
                DateOfBirth = new DateTime(1996, 10, 10),
                Gender = true,
                JoinedDate = new DateTime(2019, 1, 1),
                Type = UserConstants.Common.AdminRole,
                TimeOffset = 0 , 
            };
        }

        public static User GetEditingUser()
        {
            return new User()
            {
                Id = 1,
                StaffCode = "SD001",
                UserName = "whatthehell",
                Location = Location.HCM,
                FirstName = "",
                LastName = "",
                DateOfBirth = new DateTime(2000, 10, 1),
                Gender = true,
                JoinedDate = new DateTime(2019, 1, 1),
                Type = UserConstants.Common.AdminRole,
                IsDisabled = false,
                Email ="",
                EmailConfirmed = true,
                IsFirstChangePassword = true,
                PhoneNumber = "", 
                PhoneNumberConfirmed = true,
                ConcurrencyStamp = "1" ,
                NormalizedEmail ="" ,
                NormalizedUserName = "", 
                               
            };
        }

        public static List<User> GetUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    StaffCode = "SD0001",
                    UserName = "quangvd",
                    Location = Location.HCM,
                    FirstName = "Quang",
                    LastName = "Vo Duc",
                    DateOfBirth = new DateTime(2000, 10, 1),
                    Gender = true,
                    JoinedDate = new DateTime(2019, 1, 1),
                    Type = UserConstants.Common.StaffRole
                },
                new User()
                {
                    Id = 2,
                    StaffCode = "SD0002",
                    UserName = "dinhtl",
                    Location = Location.HCM,
                    FirstName = "Dinh",
                    LastName = "Trieu Le",
                    DateOfBirth = new DateTime(1998, 10, 1),
                    Gender = true,
                    JoinedDate = new DateTime(2013, 10, 21),
                    Type = UserConstants.Common.StaffRole
                }
            };
        }

        public static User GetUser(int id, string staffCode)
        {
            return new User()
            {
                Id = id,
                StaffCode = staffCode,
                UserName = "ann",
                Location = Location.HCM,
                FirstName = "An",
                LastName = "Nguyen",
                DateOfBirth = new DateTime(1996, 10, 1),
                Gender = true,
                JoinedDate = new DateTime(2019, 1, 1),
                Type = UserConstants.Common.StaffRole
            };
        }

        public static UserQueryCriteriaDto GetUserQueryCriteriaDto()
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

        public static User GetUserFromClaims()
        {
            return new User()
            {
                Id = 1,
                StaffCode = "SD0000",
                FirstName = "Cuong",
                LastName = "Tran",
                Location = Location.HCM,
                Type = UserConstants.Common.AdminRole
            };
        }
    }
}
