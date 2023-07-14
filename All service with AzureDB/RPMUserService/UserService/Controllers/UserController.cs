using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UserServiceRepository;

namespace UserService.Controllers
{
    class Global
    {
        public static string gentoken;
        public static string Baseurl = "https://rpmproceedtobuyservice.azurewebsites.net/ProceedToBuy/";
    }

    [Route("[controller]/[action]/")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        //public string gentoken = "";
        //private List<string> = new List<string>();
        public UserController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            userRepository = new UserRepository();
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public string GetTokenById(int id)
        {
            return userRepository.GetTokenById(id);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{username}/{password}")]
        public string Authenticate(string username, string password)
        {
            var token = jwtAuthenticationManager.Authenticate(username, password);
            if (token == null)
            {
                return "Unauthorized";
            }
            Global.gentoken = token;
            userRepository.AddToken(username, password, token);
            return token;
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostUser([FromBody]User user)
        {
            
            // Posting data to Proceedtobuy service

            using (var client = new HttpClient())
            {
                userRepository.AddUser(user);
                User u = userRepository.GetUserByNameAndPass(user.User_Name, user.Password);
                //Passing service base url  
                client.BaseAddress = new Uri(Global.Baseurl);

                client.DefaultRequestHeaders.Clear();

                var values = new Dictionary<string, string>
                {
                    { "userId", u.User_Id.ToString() },
                    { "zipCode", u.Zip_Code.ToString() }
                };
                var content = JsonConvert.SerializeObject(values);

                //Sending request to find web api REST service resource GetAllOffers using HttpClient  
                HttpResponseMessage Res = await client.PostAsync("AddMini", new StringContent(content, Encoding.UTF8, "application/json"));

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    
                    return "User Created";
                }
                else
                {
                    return "Error! User not created";
                }
            }
        }
        [HttpGet]
        [Route("{name}/{pass}")]
        public User GetUserByNameAndPass(string name,string pass)
        {
            return userRepository.GetUserByNameAndPass(name,pass);
        }
        [HttpGet]
        public User GetUserById(int id)
        {
            return userRepository.GetUserById(id);
        }
    }
}
