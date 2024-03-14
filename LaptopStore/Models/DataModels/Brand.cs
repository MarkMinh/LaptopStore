using System;
using System.Collections.Generic;

namespace LaptopStore.Models.DataModels
{
    public partial class Brand
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string BrandName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
