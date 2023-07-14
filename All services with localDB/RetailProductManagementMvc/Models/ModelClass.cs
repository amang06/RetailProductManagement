using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailProductManagementMvc.Models
{
    public class ModelClass
    {
        public List<Product_Rating> product_Rating_list { get; set; }
        public Product product { get; set; }
        public Wishlist wishlist { get; set; }
        public Cart cart { get; set; }
    }
}
