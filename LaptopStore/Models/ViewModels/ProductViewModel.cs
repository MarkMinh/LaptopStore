namespace LaptopStore.Models.ViewModels
{
    public class ProductViewModel
    {

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
        public IFormFile? Image { get; set; }
    }
}
