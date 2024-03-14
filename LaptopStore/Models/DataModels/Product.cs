using System;
using System.Collections.Generic;

namespace LaptopStore.Models.DataModels
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? UnitsInStock { get; set; }
        public decimal? Discount { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? Description { get; set; }
        public decimal? Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int? SoldNumber { get; set; }
        public int? ReviewId { get; set; }
        public string? Image { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Review? Review { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
