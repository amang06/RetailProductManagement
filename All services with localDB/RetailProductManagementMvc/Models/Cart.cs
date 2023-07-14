using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailProductManagementMvc.Models
{
    public class Cart
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int ZipCode { get; set; }
        public DateTime ExpectedDelivery { get; set; }
        public string ProductName { get; set; }
    }
}
