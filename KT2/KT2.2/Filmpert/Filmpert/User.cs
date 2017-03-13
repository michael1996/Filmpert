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

        public int Type
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
        {
            
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
