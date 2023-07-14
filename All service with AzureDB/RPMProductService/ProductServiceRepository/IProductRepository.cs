using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServiceRepository
{
    public interface IProductRepository
    {
        public Product GetProductById(int id);
        public Product GetProductByName(string name);
        public List<Product> GetAllProducts();
    }
}
