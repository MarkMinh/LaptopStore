using System;
using System.Collections.Generic;

namespace LaptopStore.Models.DataModels
{
    public partial class Review
    {
        public Review()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
