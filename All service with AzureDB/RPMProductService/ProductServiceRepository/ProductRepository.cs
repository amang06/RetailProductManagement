using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductServiceRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDBContext con;
        public ProductRepository()
        {
            con = new ProductDBContext();
        }
        public List<Product> GetAllProducts()
        {
            return con.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            Product product = (from p in con.Products where p.Product_Id == id select p).FirstOrDefault();
            return product;
        }

        public Product GetProductByName(string name)
        {
            Product product = (from p in con.Products where p.Product_Name == name select p).FirstOrDefault();
            return product;
        }
    }
}
