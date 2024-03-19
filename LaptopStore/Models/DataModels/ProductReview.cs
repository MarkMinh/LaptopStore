using System;
using System.Collections.Generic;

namespace LaptopStore.Models.DataModels
{
    public partial class ProductReview
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? Username { get; set; }
        public int? Rating { get; set; }
        public string? Description { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Account? UsernameNavigation { get; set; }
    }
}
