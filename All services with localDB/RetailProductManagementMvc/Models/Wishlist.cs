﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailProductManagementMvc.Models
{
    public class Wishlist
    {
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
