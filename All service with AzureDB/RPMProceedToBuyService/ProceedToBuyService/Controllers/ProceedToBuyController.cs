using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ProceedToBuyLibrary;
using Microsoft.AspNetCore.Authorization;

namespace ProceedToBuyService.Controllers
{
    class Global
    {
        public static string userToken;
        public static string Baseurl = "https://rpmvendorservice.azurewebsites.net/Vendor/";
    }
    [Authorize]
    [Route("[controller]/[action]/")]
    [ApiController]
    public class ProceedToBuyController : ControllerBase
    {

        private readonly IProceedToBuyRepository ptbRepository;
        public ProceedToBuyController()
        {
            ptbRepository = new ProceedToBuyRepository();
        }
        [HttpPost]
        public string PostWishlist([FromBody] Wishlist product)
        {
            product.DateAdded = DateTime.Today;
            ptbRepository.AddToWishlist(product);
            return "Added To Wishlist";
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostCart([FromBody] Cart product)
        {
            
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Global.Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api VendorService resource GetStockByProductId using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("GetStockByProductId/" + product.ProductId);
                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Offer list  
                    int stock = JsonConvert.DeserializeObject<int>(response);
                    if (stock > 0)
                    {
                        product.ExpectedDelivery = DateTime.Today.AddDays(2);
                        product.ZipCode = ptbRepository.GetZipCode(product.UserId);
                        ptbRepository.AddToCart(product);
                        return "Added To Cart";
                    }
                    else
                    {
                        HttpResponseMessage Res2 = await client.GetAsync("GetStockReplenishmentDateByProductId/" + product.ProductId);
                        if (Res2.IsSuccessStatusCode)
                        {
                            var response2 = Res2.Content.ReadAsStringAsync().Result;
                            //Deserializing the response recieved from web api and storing into the Offer list  
                            DateTime date = JsonConvert.DeserializeObject<DateTime>(response2);

                            return "Product Out of Stock. Next stock will arrive on " + date.Date + " Please add to Wishlist!";
                        }
                        else
                        {
                            return "Product Out of Stock. Please add to Wishlist!";
                        }
                    }
                }
                else
                {
                    return "Did not receive response from Vendor Service";
                }
            }

        }
        [AllowAnonymous]
        [HttpPost]
        public void AddMiniUser([FromBody]MiniUser mu)
        {
            ptbRepository.AddMiniUser(mu);
        }
        [Route("{id}")]
        [HttpGet]
        public List<Wishlist> GetWishlistByUserId(int id)
        {
            return ptbRepository.GetWishlistByUserId(id);
        }
        [Route("{id}")]
        [HttpGet]
        public List<Cart>  GetCartByUserId(int id)
        {
            return ptbRepository.GetCartByUserId(id);
        }
        [AllowAnonymous]
        [HttpGet]
        public string Apiworking()
        {
            return "Yes";
        }
    }
}
