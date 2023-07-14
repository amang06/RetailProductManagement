using ProductServiceRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServiceRepository
{
    public interface IProductRatingRepository
    {
        public void AddProductRating(Product_Rating pr);
        public List<Product_Rating> GetProductRatingsByProductId(int pid);
    }
}
