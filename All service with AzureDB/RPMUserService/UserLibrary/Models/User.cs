using System;
using System.Collections.Generic;

#nullable disable

namespace UserServiceRepository
{
    public partial class User
    {
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public decimal? Zip_Code { get; set; }
        public string Token { get; set; }
    }
}
