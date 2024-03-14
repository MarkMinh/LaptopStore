using System;
using System.Collections.Generic;

namespace LaptopStore.Models.DataModels
{
    public partial class Account
    {
        public Account()
        {
            Orders = new HashSet<Order>();
        }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int RoleId { get; set; }
        public bool Enabled { get; set; }
        public bool Verify { get; set; }
        public string? OtpCode { get; set; }
        public DateTime? TimeOtpCreated { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Profile Username1 { get; set; } = null!;
        public virtual Cart UsernameNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
