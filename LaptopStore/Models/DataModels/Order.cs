using System;
using System.Collections.Generic;

namespace LaptopStore.Models.DataModels
{
    public partial class Order
    {
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

        public virtual Product Product { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
        public virtual Account UsernameNavigation { get; set; } = null!;
        public virtual ICollection<CartOrder> CartOrders { get; set; }
    }
}
