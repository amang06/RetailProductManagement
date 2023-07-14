using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetailProductManagementMvc.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RetailProductManagementMvc.Controllers
{
    public class WishlistController : Controller
    {
        private readonly JwtSecurityTokenHandler handler;
        public WishlistController()
        {
            handler = new JwtSecurityTokenHandler();
        }

        public async Task<IActionResult> Index(int id)
        {
            if (HttpContext.Session.GetString("token") != null && handler.ReadJwtToken(HttpContext.Session.GetString("token")).ValidTo > DateTime.UtcNow)
            {
                var response = await GetWishlist(Convert.ToInt32(HttpContext.Session.GetString("Id")));
                List<Wishlist> lists = new List<Wishlist>();
                lists = JsonConvert.DeserializeObject<List<Wishlist>>(response);
                ViewBag.Name = HttpContext.Session.GetString("Name");
                return View(lists);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<string> GetWishlist(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Global.Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                try
                {
                    HttpResponseMessage Res = await client.GetAsync("GetWishlist/" + id);
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

        public async Task<IActionResult> AddToWishlist(ModelClass mc)
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
                    { "ProductName", mc.wishlist.ProductName },
                    { "ProductId", mc.wishlist.ProductId.ToString() },
                    { "Quantity", mc.wishlist.Quantity.ToString() }
                };
                var content = JsonConvert.SerializeObject(values);
                try
                {
                    HttpResponseMessage Res = await client.PostAsync("AddToWishlist", new StringContent(content, Encoding.UTF8, "application/json"));
                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index","Event", new { message = "Added to Wishlist!" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Event", new { message = "Already added to wishlist!" });
                    }
                }
                catch
                {
                    return RedirectToAction("Error", "Event", new { message = "Unknown Error has occured!" });
                }
            }
        }
    }
}
