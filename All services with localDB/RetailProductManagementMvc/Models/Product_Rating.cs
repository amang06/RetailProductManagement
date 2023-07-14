using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailProductManagementMvc.Models
{
    public class Product_Rating
    {
        public int Product_Id { get; set; }
        public int User_Id { get; set; }
        public int Rating { get; set; }
        public string User_Name { get; set; }
    }
}
