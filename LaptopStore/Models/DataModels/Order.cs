using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaptopStore.Models.DataModels
{
    public partial class Order
    {
        LaptopStoreContext db = new LaptopStoreContext();
        public Order()
        {
            CartOrders = new HashSet<CartOrder>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int StatusId { get; set; }

		public virtual Product Product { get; set; }
		public virtual Status Status { get; set; } = null!;
		public virtual Account UsernameNavigation { get; set; } = null!;
		public virtual ICollection<CartOrder> CartOrders { get; set; }

		[NotMapped]
        public virtual Product Product2
        {
            get
            {
                return db.Products.FirstOrDefault(x => x.Id == ProductId);
            }
            set {  }
        }
        [NotMapped]
		public decimal TotalPrice => Product2?.UnitPrice * Quantity ?? 0;

		
    }
}
