using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ProductServiceRepository
{
    public class ProductRatingRepository : IProductRatingRepository
    {
        private readonly ProductDBContext con;
        public ProductRatingRepository()
        {
            con = new ProductDBContext();
        }
        public void AddProductRating(Product_Rating pr)
        {
            con.Product_Ratings.Add(pr);
            con.SaveChanges();
        }

        public List<Product_Rating> GetProductRatingsByProductId(int pid)
        {
            List<Product_Rating> product_Ratings = new List<Product_Rating>();
            product_Ratings = (from pr in con.Product_Ratings where pr.Product_Id == pid select pr).ToList();
            return product_Ratings;
        }
    }
}
