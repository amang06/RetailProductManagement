using System;
using System.Collections.Generic;
using System.Text;

namespace UserServiceRepository
{
    public interface IUserRepository
    {
        public void AddUser(User user);
        public User GetUserByNameAndPass(string name,string pass);
        public User GetUserById(int id);
        public string GetTokenById(int id);
        public void AddToken(string username, string password, string tok);
    }
}
