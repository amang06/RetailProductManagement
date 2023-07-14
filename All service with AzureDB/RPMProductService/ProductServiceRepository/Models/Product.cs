using System;
using System.Collections.Generic;

#nullable disable

namespace ProductServiceRepository
{
    public partial class Product
    {
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Description { get; set; }
        public string Image_Name { get; set; }
        public int? Vendor_Id { get; set; }
    }
}
