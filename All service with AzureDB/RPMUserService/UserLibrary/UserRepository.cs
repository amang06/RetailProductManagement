using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserServiceRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDBContext con;
        public UserRepository()
        {
            con = new UserDBContext();
        }
        public void AddUser(User user)
        {
            con.Users.Add(user);
            con.SaveChanges();
        }
        public User GetUserByNameAndPass(string name,string pass)
        {
            User user = (from u in con.Users where u.User_Name == name && u.Password == pass select u).FirstOrDefault();
            return user;
        }
        public User GetUserById(int id)
        {
            User user = (from u in con.Users where u.User_Id == id select u).FirstOrDefault();
            return user;
        }
        public string GetTokenById(int id)
        {
            string tok = (from u in con.Users where u.User_Id == id select u.Token).First();
            return tok;
        }
        public void AddToken(string username,string password,string tok)
        {
            User user = (from u in con.Users where u.User_Name == username && u.Password == password select u).First();
            user.Token = tok;
            con.SaveChanges();
        }
    }
}
