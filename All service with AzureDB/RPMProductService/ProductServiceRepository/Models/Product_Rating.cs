using System;
using System.Collections.Generic;

#nullable disable

namespace ProductServiceRepository
{
    public partial class Product_Rating
    {
        public int Product_Id { get; set; }
        public int User_Id { get; set; }
        public int? Rating { get; set; }
        public string User_Name { get; set; }
    }
}
