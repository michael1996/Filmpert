using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmpert
{
    class Actor
    {
        public int ActorId
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public DateTime Birthday
        {
            get;
            private set;
        }

        public Actor()
            : this(0, "", new DateTime())
        {

        }

        public Actor(int id, string name, DateTime birthday)
        {
            ActorId = id;
            Name = name;
            Birthday = birthday;
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
