using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmpert
{
    class UserFavourite
    {
        public int UserId
        {
            get;
            private set;
        }

        public int MovieId
        {
            get;
            private set;
        }

        public UserFavourite()
            : this (0, 0)
        {

        }

        public UserFavourite(int userId, int movieId)
        {
            UserId = userId;
            MovieId = movieId;
        }

        public void Add()
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
