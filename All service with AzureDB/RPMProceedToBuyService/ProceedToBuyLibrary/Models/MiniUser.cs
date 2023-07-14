using System;
using System.Collections.Generic;

#nullable disable

namespace ProceedToBuyLibrary
{
    public partial class MiniUser
    {
        public MiniUser()
        {
            Carts = new HashSet<Cart>();
        }

        public int UserId { get; set; }
        public decimal ZipCode { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
    }
}
