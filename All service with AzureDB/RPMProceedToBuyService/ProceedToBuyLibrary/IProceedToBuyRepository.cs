using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceedToBuyLibrary
{
    public interface IProceedToBuyRepository
    {
        void AddToWishlist(Wishlist product);
        List<Wishlist> GetWishlistByUserId(int userid);
        void AddToCart(Cart product);
        List<Cart> GetCartByUserId(int userid);
        void AddMiniUser(MiniUser user);
        int GetZipCode(int userid);
    }
}
