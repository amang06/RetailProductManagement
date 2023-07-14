using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductServiceRepository;
using Microsoft.AspNetCore.Authorization;

namespace ProductService.Controllers
{
    [Route("[controller]/[action]/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IProductRatingRepository productRatingRepository;
        public ProductController()
        {
            productRepository = new ProductRepository();
            productRatingRepository = new ProductRatingRepository();
        }

        [HttpGet]
        [Route("{id}")]
        public Product GetById(int id)
        {
            return productRepository.GetProductById(id);
        }

        [HttpGet]
        [Route("{name}")]
        public Product GetByName(string name)
        {
            return productRepository.GetProductByName(name);
        }

        [HttpGet]
        public List<Product> GetAll()
        {
            return productRepository.GetAllProducts();
        }
        [Authorize]
        [HttpPost]
        public void AddRating([FromBody]Product_Rating pr)
        {
            productRatingRepository.AddProductRating(pr);
        }

        [HttpGet]
        [Route("{id}")]
        public List<Product_Rating> GetRatings(int id)
        {
            return productRatingRepository.GetProductRatingsByProductId(id);
        }
    }
}
