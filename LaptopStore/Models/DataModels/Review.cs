using System;
using System.Collections.Generic;

namespace LaptopStore.Models.DataModels
{
    public partial class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Description { get; set; }
    }
}
