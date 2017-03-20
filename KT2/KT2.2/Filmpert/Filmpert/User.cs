using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmpert
{
    public enum UserType { ADMIN, NORMAL };

    class User
    {
        public int UserId
        {
            get;
            private set;
        }

        public string Username
        {
            get;
            private set;
        }

        public string Password
        {
            get;
            private set;
        }

        public string Email
        {
            get;
            private set;
        }

        public DateTime Birthday
        {
            get;
            private set;
        }

        public UserType Type
        {
            get;
            private set;
        }

        public bool LoggedIn
        {
            get;
            private set;
        }

        public User()
            : this(0, "", "", "", new DateTime(), (UserType)1, false)
        {
            
        }

        public User(int id, string username, string password, string email, DateTime birthday, UserType type, bool loggedIn)
        {
            UserId = id;
            Username = username;
            Password = password;
            Email = email;
            Birthday = birthday;
            Type = type;
            LoggedIn = loggedIn;
        }

        public void Add()
        {

        }

        public void Modify()
        {

        }

        public void Delete()
        {

        }

        public void View()
        {

        }
    }
}
