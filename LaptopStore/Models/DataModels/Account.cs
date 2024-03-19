using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaptopStore.Models.DataModels
{
    public partial class Account
    {
        public Account()
        {
            Orders = new HashSet<Order>();
            ProductReviews = new HashSet<ProductReview>();
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
        public virtual Cart Cart { get; set; } = null!;
        public virtual Profile Profile { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        [NotMapped]
        public string rePass { get; set; }
        [NotMapped]
        public string newPass { get; set; }
    }
}
