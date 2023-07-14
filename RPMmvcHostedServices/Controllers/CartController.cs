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
    public class CartController : Controller
    {
        private readonly JwtSecurityTokenHandler handler;
        public CartController()
        {
            handler = new JwtSecurityTokenHandler();
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("token") != null && handler.ReadJwtToken(HttpContext.Session.GetString("token")).ValidTo > DateTime.UtcNow)
            {
                var response = await GetCart(Convert.ToInt32(HttpContext.Session.GetString("Id")));
                List<Cart> carts = new List<Cart>();
                carts = JsonConvert.DeserializeObject<List<Cart>>(response);
                ViewBag.Name = HttpContext.Session.GetString("Name");
                return View(carts);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<string> GetCart(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Global.Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                try
                {
                    HttpResponseMessage Res = await client.GetAsync("GetCart/" + id);
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
        public async Task<IActionResult> AddToCart(ModelClass mc)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Global.Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                var values = new Dictionary<string, string>
                {
                    { "UserId", HttpContext.Session.GetString("Id") },
                    { "ProductName", mc.cart.ProductName },
                    { "ProductId", mc.cart.ProductId.ToString() },
                    { "Quantity", mc.cart.Quantity.ToString() }
                };
                var content = JsonConvert.SerializeObject(values);
                try
                {
                    HttpResponseMessage Res = await client.PostAsync("AddToCart", new StringContent(content, Encoding.UTF8, "application/json"));
                    if (Res.IsSuccessStatusCode)
                    {
                        var response = Res.Content.ReadAsStringAsync().Result;
                        string s = JsonConvert.DeserializeObject<string>(response);
                        if(s.ToString() == "Added To Cart")
                        {
                            return RedirectToAction("Index", "Event", new { message = "Added to Cart!" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Event", new { message = s.ToString() });
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Event", new { message = "Already added to Cart!" });
                    }
                }
                catch
                {
                    return RedirectToAction("Error", "Event", new { message = "Unknown error has occured!" });
                }
            }
        }
    }
}
