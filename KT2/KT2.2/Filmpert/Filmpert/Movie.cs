using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmpert
{
    class Movie
    {
        public int MovieId
        {
            get;
            private set;
        }

        public string Title
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;
        }

        public DateTime ReleaseDate
        {
            get;
            private set;
        }

        public string Poster
        {
            get;
            private set;
        }

        public string Trailer
        {
            get;
            private set;
        }

        public Movie()
            : this (0, "", "", new DateTime(), "", "")
        {

        }

        public Movie(int id, string title, string description, DateTime release, string poster, string trailer)
        {
            MovieId = id;
            Title = title;
            Description = description;
            ReleaseDate = release;
            Poster = poster;
            Trailer = trailer;
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
