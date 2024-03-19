using System;
using System.Collections.Generic;

namespace LaptopStore.Models.DataModels
{
    public partial class Cart
    {
        public Cart()
        {
            CartOrders = new HashSet<CartOrder>();
        }

        public string Username { get; set; } = null!;
        public decimal TotalPrice { get; set; }

        public virtual Account UsernameNavigation { get; set; } = null!;
        public virtual ICollection<CartOrder> CartOrders { get; set; }
    }
}
