using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RetailProductManagementMvc.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RetailProductManagementMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly JwtSecurityTokenHandler handler;

        public ProductController()
        {
            handler = new JwtSecurityTokenHandler();
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("token") != null && handler.ReadJwtToken(HttpContext.Session.GetString("token")).ValidTo > DateTime.UtcNow)
            {
                List<Product> productList = new List<Product>();
                var response = await GetProducts();
                if (response.ToString() == "Error!")
                {
                    Product p = new Product();
                    p.Product_Name = "none";
                    productList.Add(p);
                }
                else
                {
                    productList = JsonConvert.DeserializeObject<List<Product>>(response);
                }
                ViewBag.Name = HttpContext.Session.GetString("Name");
                return View(productList);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ProductDetails(int id)
        {

            if (HttpContext.Session.GetString("token") != null && handler.ReadJwtToken(HttpContext.Session.GetString("token")).ValidTo > DateTime.UtcNow)
            {
                Product product = new Product();
                ModelClass mc = new ModelClass();
                var response = await GetProductById(id);
                var response2 = await GetRating(id);
                if (response.ToString() == "")
                {
                    return RedirectToAction("Index", "Event", new { message = "No such product. Please try again!" });
                }
                else
                {
                    ViewBag.Name = HttpContext.Session.GetString("Name");
                    
                    mc.product_Rating_list = JsonConvert.DeserializeObject<List<Product_Rating>>(response2);
                    mc.product = JsonConvert.DeserializeObject<Product>(response);
                }

                return View(mc);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddRating(int id,string name)
        {
            if (HttpContext.Session.GetString("token") != null && handler.ReadJwtToken(HttpContext.Session.GetString("token")).ValidTo > DateTime.UtcNow)
            {
                ViewBag.ProductId = id;
                ViewBag.ProductName = name;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SearchProduct()
        {
            if (HttpContext.Session.GetString("token") != null && handler.ReadJwtToken(HttpContext.Session.GetString("token")).ValidTo > DateTime.UtcNow)
            {
                ViewBag.Name = HttpContext.Session.GetString("Name");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> SearchProductByName(Search search)
        {
            if (HttpContext.Session.GetString("token") != null && handler.ReadJwtToken(HttpContext.Session.GetString("token")).ValidTo > DateTime.UtcNow)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Global.Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    try
                    {
                        Product p = new Product();
                        HttpResponseMessage Res = await client.GetAsync("ProductByName/"+search.ProductName);
                        if (Res.IsSuccessStatusCode)
                        {
                            var response = Res.Content.ReadAsStringAsync().Result;
                            if(response == "")
                            {
                                return RedirectToAction("Index", "Event", new { message = "No such product. Please try again!" });
                            }
                            p = JsonConvert.DeserializeObject<Product>(response);
                            return RedirectToAction("ProductDetails", "Product", new { id = p.Product_Id });
                        }
                        else
                        {
                            return RedirectToAction("Error", "Event", new { message = "Error in API call!" });
                        }
                    }
                    catch
                    {
                        return RedirectToAction("Error", "Event", new { message = "Unknown Error has occured!" });
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SearchProductById(Search search)
        {
            if (HttpContext.Session.GetString("token") != null && handler.ReadJwtToken(HttpContext.Session.GetString("token")).ValidTo > DateTime.UtcNow)
            {
                return RedirectToAction("ProductDetails", "Product", new { id = search.ProductId });
            }
            
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> AddRatingForProduct(Product_Rating pr)
        {
            

            if (HttpContext.Session.GetString("token") != null && handler.ReadJwtToken(HttpContext.Session.GetString("token")).ValidTo > DateTime.UtcNow)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Global.Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    var values = new Dictionary<string, string>
                    {
                    { "Product_Id", pr.Product_Id.ToString() },
                    { "User_Id", HttpContext.Session.GetString("Id") },
                    { "Rating", pr.Rating.ToString() },
                    { "User_Name", HttpContext.Session.GetString("Name") }
                    };
                    var content = JsonConvert.SerializeObject(values);
                    try
                    {
                        HttpResponseMessage Res = await client.PostAsync("AddRating", new StringContent(content, Encoding.UTF8, "application/json"));
                        if (Res.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "Event", new { message = "Rating Added" });
                        }
                        else
                        {
                            return RedirectToAction("Error!", "Event", new { message = "Error while adding rating! Please try again!" });
                        }
                    }
                    catch
                    {
                        return RedirectToAction("Error!", "Event", new { message = "Unknown Error has occured!" });
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        private async Task<string> GetProductById(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Global.Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage Res = await client.GetAsync("ProductById/"+id);
                    if (Res.IsSuccessStatusCode)
                    {
                        return Res.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        return "Error!";
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
        private async Task<string> GetProducts()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Global.Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage Res = await client.GetAsync("AllProducts");
                    if (Res.IsSuccessStatusCode)
                    {
                        return Res.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        return "Error!";
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
        private async Task<string> GetRating(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Global.Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage Res = await client.GetAsync("AllRatings/"+id);
                    if (Res.IsSuccessStatusCode)
                    {
                        return Res.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        return "Error!";
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
