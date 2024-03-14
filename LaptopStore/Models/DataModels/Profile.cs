using System;
using System.Collections.Generic;

namespace LaptopStore.Models.DataModels
{
    public partial class Profile
    {
        public string Username { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public DateTime? Dob { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
