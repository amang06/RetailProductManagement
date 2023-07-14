using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetailProductManagementMvc.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace RetailProductManagementMvc.Controllers
{
    class Global
    {
        public static readonly string Baseurl = "http://localhost:63818";
    }
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly JwtSecurityTokenHandler handler;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            handler = new JwtSecurityTokenHandler();
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("token") != null && handler.ReadJwtToken(HttpContext.Session.GetString("token")).ValidTo > DateTime.UtcNow)
            {
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(User u)
        {
            var r = await Loginmethod(u.User_Name, u.Password);
            if(r.ToString() == "Unauthorized!")
            {
                return RedirectToAction("Error", "Event", new { message = "Unauthorized! " } );
            }
            if(r.ToString() == "Error in API Call!")
            {
                return RedirectToAction("Error", "Event", new { message = "Error in API Call!" });
            }
            if(r.ToString() == "API not responding!")
            {
                return RedirectToAction("Error", "Event", new { message = "API not responding!" });
            }
            return RedirectToAction("Index", "Product");
            //return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<string> Loginmethod(string username,string password)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Global.Baseurl);

                client.DefaultRequestHeaders.Clear();
                try
                {
                    HttpResponseMessage Res = await client.GetAsync("Authenticate/" + username + "/" + password);

                    if (Res.IsSuccessStatusCode)
                    {
                        var response = Res.Content.ReadAsStringAsync().Result;
                        if(response == "Unauthorized")
                        {
                            return "Unauthorized!";
                        }
                        else
                        {
                            HttpResponseMessage Res2 = await client.GetAsync("GetUser/" + username + "/" + password);
                            if (Res2.IsSuccessStatusCode)
                            {

                                var response2 = Res2.Content.ReadAsStringAsync().Result;
                                User u = new User();
                                u = JsonConvert.DeserializeObject<User>(response2);
                                HttpContext.Session.SetString("Id", u.User_Id.ToString());
                                HttpContext.Session.SetString("Name", u.User_Name);
                                HttpContext.Session.SetString("token", response);
                                return response;
                            }
                            else
                            {
                                return "Error in API Call!";
                            }
                        }
                        
                    }
                    else
                    {
                        return "Error in API Call!";
                    }
                }
                catch
                {
                    return "API not responding!";
                }
            }
        }
    }
}
