using System;

namespace Rookie.AssetManagement.Contracts.Dtos.UserDtos
{
    public class UserCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public DateTime JoinedDate { get; set; }
        public string Type { get; set; }
        public int TimeOffset { get; set; }
    }
}
