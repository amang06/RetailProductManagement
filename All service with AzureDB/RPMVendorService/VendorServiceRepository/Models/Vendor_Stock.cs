using System;
using System.Collections.Generic;

#nullable disable

namespace VendorServiceRepository
{
    public partial class Vendor_Stock
    {
        public int Product_Id { get; set; }
        public int Vendor_Id { get; set; }
        public int? Stock_In_Hand { get; set; }
        public DateTime? Expected_StockReplenish_Date { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
