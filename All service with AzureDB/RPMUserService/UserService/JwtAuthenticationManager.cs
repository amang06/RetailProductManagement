using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using UserServiceRepository;

namespace UserService
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly UserDBContext con;
        private readonly string key;
        public JwtAuthenticationManager(string key)
        {
            con = new UserDBContext();
            this.key = key;
        }
        public string Authenticate(string username, string password)
        {
            List<User> userList = new List<User>();
            userList = con.Users.ToList();
            var res = userList.ToDictionary(key => key.User_Name, key => key.Password);

            if(!userList.Any(u => u.User_Name == username && u.Password == password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
