using System;
using System.Collections.Generic;

#nullable disable

namespace ProceedToBuyLibrary
{
    public partial class Wishlist
    {
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
