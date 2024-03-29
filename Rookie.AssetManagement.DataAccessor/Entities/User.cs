﻿using Microsoft.AspNetCore.Identity;
using Rookie.AssetManagement.Contracts.Enums;
using System;
using System.Collections.Generic;

namespace Rookie.AssetManagement.DataAccessor.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoinedDate { get; set; }
        public bool Gender { get; set; }
        public string Type { get; set; }
        public string StaffCode { get; set; }
        override public string UserName { get; set; }
        public Location Location { get; set; }
        public bool IsFirstChangePassword { get; set; }
        public bool IsDisabled { get; set; }
        public virtual ICollection<ReturnRequest> ReturnsRequest { get; set; }
        public virtual ICollection<ReturnRequest> ReturnsAccept { get; set; }
        public virtual ICollection<Assignment> AssignmentsTo { get; set; }
        public virtual ICollection<Assignment> AssignmentsBy { get; set; }
    }
}
