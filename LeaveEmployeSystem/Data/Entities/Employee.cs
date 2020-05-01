using Microsoft.AspNetCore.Identity;
using System;

namespace LeaveEmployeSystem.Data.Entities
{
    public class Employee : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string TaxID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }

    }
}
