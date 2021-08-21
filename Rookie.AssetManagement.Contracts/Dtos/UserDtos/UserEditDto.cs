using System;

namespace Rookie.AssetManagement.Contracts.Dtos.UserDtos
{
    public class UserEditDto
    {
        public int Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public DateTime JoinedDate { get; set; }
        public string Type { get; set; }
        public int TimeOffset { get; set; }
    }
}
