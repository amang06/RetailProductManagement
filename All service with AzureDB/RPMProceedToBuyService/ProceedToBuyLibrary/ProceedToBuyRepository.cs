using System;
using System.Collections.Generic;
using System.Linq;
namespace ProceedToBuyLibrary
{
    public class ProceedToBuyRepository : IProceedToBuyRepository
    {
        private readonly ProceedToBuyDBContext _context;
        public ProceedToBuyRepository()
        {
            _context = new ProceedToBuyDBContext();
        }
        public void AddMiniUser(MiniUser user)
        {
            _context.MiniUsers.Add(user);
            _context.SaveChanges();
        }

        public void AddToCart(Cart product)
        {
            _context.Carts.Add(product);
            _context.SaveChanges();
        }

        public void AddToWishlist(Wishlist product)
        {
            _context.Wishlists.Add(product);
            _context.SaveChanges();
        }
        public int GetZipCode(int userid)
        {
            int zipcode = (from user in _context.MiniUsers where user.UserId == userid select (int)user.ZipCode).First();
            return zipcode;
        }
        public List<Wishlist> GetWishlistByUserId(int userid)
        {
            List <Wishlist> wishlist = (from p in _context.Wishlists where p.UserId == userid select p).ToList();
            return wishlist;
        }
        public List<Cart> GetCartByUserId(int userid)
        {
            List<Cart> cart = (from c in _context.Carts where c.UserId == userid select c).ToList();
            return cart;
        }
    }
}
