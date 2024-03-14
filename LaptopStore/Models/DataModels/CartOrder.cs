using System;
using System.Collections.Generic;

namespace LaptopStore.Models.DataModels
{
    public partial class CartOrder
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public int OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Cart UsernameNavigation { get; set; } = null!;
    }
}
