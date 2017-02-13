using Filmfy.Helpers;
using Filmfy.JsonConverters;
using FilmfyLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Filmfy.Models
{
	public class Movie : BaseModel, IBaseMovie
	{
		/// <summary>
		/// Title of the Movie
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Description of the Movie
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Release Date of the Movie
		/// </summary>
		public DateTime ReleaseDate { get; set; }

		/// <summary>
		/// Link to the Poster
		/// </summary>
		public string Poster { get; set; }

		/// <summary>
		/// Link to the Trailer
		/// </summary>
		public string Trailer { get; set; }

		/// <summary>
		/// Gets the release date to a correct string.
		/// </summary>
		public string ReleaseDateToString
		{
			get
			{
				return ReleaseDate.ToString("D");
			}
		}

		/// <summary>
		/// Actors of the Movie
		/// </summary>
		[JsonConverter(typeof(ActorConverter<Actor>))]
		public List<IBaseActor> Actors { get; set; }

		public Movie() : base() { }

		public Movie(int id) : base(id)
		{ }

		/// <summary>
		/// Loads all the Movies synchronously
		/// </summary>
		/// <returns>
		/// Loads all the movies
		/// </returns>
		public List<Movie> GetAll()
		{
			List<Movie> movies = new List<Movie>();
			movies = Task.Run(GetAllAsync).Result;
			return movies;
		}

		/// <summary>
		/// Loads all the movies asynchronous
		/// </summary>
		/// <returns></returns>
		public async Task<List<Movie>> GetAllAsync()
		{
			List<Movie> movies = new List<Movie>();
			string url = ApiHelper.GetUrl("Movie/GetAll");
			HttpWebRequest request = WebRequest.CreateHttp(url);
			Task<WebResponse> responseTask = request.GetResponseAsync();

			Task continueTask = responseTask.ContinueWith((x) =>
			{
				string body = new StreamReader(x.Result.GetResponseStream()).ReadToEnd();
				List<Movie> loadedMovies = JsonConvert.DeserializeObject<List<Movie>>(body);
				movies = loadedMovies;
			});

			await continueTask;

			return movies;
		}
	}
}
