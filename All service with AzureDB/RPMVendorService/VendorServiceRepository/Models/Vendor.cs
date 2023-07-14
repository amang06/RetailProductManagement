using System;
using System.Collections.Generic;

#nullable disable

namespace VendorServiceRepository
{
    public partial class Vendor
    {
        public Vendor()
        {
            Vendor_Stocks = new HashSet<Vendor_Stock>();
        }

        public int Vendor_Id { get; set; }
        public string Vendor_Name { get; set; }
        public int? Delivery_Charge { get; set; }
        public int? Vendor_Rating { get; set; }
        public int? Vendor_Zip_Code { get; set; }

        public virtual ICollection<Vendor_Stock> Vendor_Stocks { get; set; }
    }
}
