using System;
using System.Collections.Generic;

#nullable disable

namespace ProceedToBuyLibrary
{
    public partial class Cart
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? ZipCode { get; set; }
        public DateTime? ExpectedDelivery { get; set; }
        public string ProductName { get; set; }

        public virtual MiniUser User { get; set; }
    }
}
