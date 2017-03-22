using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmpert
{
    class MovieActor
    {
        public int MovieId
        {
            get;
            private set;
        }

        public int ActorId
        {
            get;
            private set;
        }

        public MovieActor()
            : this (0, 0)
        {

        }

        public MovieActor(int movieId, int actorId)
        {
            MovieId = movieId;
            ActorId = actorId;
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
