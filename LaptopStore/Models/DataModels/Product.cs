using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
namespace LaptopStore.Models.DataModels
{
    public partial class Product
    {
        public Product()
        {
            
            Orders = new HashSet<Order>();
            ProductReviews = new HashSet<ProductReview>();
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
        public string? Image { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }

        [NotMapped]
        public IFormFile? ImageUpLoad { get; set; }
    }
}
