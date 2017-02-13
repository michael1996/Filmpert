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
	public class User : BaseModel, IBaseUser
	{
		/// <summary>
		/// Username of the User
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// The password the user uses to login
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Email of the user
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Date of birth of the User
		/// </summary>
		public DateTime Birthday { get; set; }

		/// <summary>
		/// What kind of the user is.
		/// </summary>
		public UserType Type { get; set; }

		/// <summary>
		/// Logged in or not
		/// </summary>
		public bool LoggedIn { get; set; }

		/// <summary>
		/// List of favourites of the User
		/// </summary>
		[JsonConverter(typeof(FavouriteConverter<Movie>))]
		public List<IBaseMovie> Favourites { get; set; }

		public User() : base() { }

		public User(int id) : base(id)
		{
			Get();
		}

		public void Login()
		{
			Task.Run(LoginAsync);
		}

		/// <summary>
		/// Logs the User in.
		/// </summary>
		public async Task LoginAsync()
		{
			using (var client = new HttpClient())
			{
				UserModel usermodel = new UserModel { Username = this.Username, Password = this.Password };

				HttpContent content = new StringContent(JsonConvert.SerializeObject(this), Encoding.UTF8, "application/json");

				Task<HttpResponseMessage> responseTask = client.PostAsync(ApiHelper.GetUrl("User/Login"), content);
				Task continueTask = responseTask.ContinueWith(async (x) =>
				{
					// Continue here with the code you want to do after the request is done!

					string body = await x.Result.Content.ReadAsStringAsync();
					User user = JsonConvert.DeserializeObject<User>(body);
					SetProperties(user);
				}, TaskScheduler.FromCurrentSynchronizationContext());

				await continueTask;
			}
		}

		/// <summary>
		/// Logs the User out.
		/// </summary>
		public void Logout()
		{
			Task.Run(LogoutAsync);
		}

		public async Task LogoutAsync()
		{
			string url = ApiHelper.GetUrl("User/Logout/" + this.ID);
			HttpWebRequest request = WebRequest.CreateHttp(url);
			Task<WebResponse> responseTask = request.GetResponseAsync();
			await responseTask;
		}

		public async void Get()
		{
			string url = ApiHelper.GetUrl("User/Get/" + this.ID);
			HttpWebRequest request = WebRequest.CreateHttp(url);
			Task<WebResponse> responseTask = request.GetResponseAsync();

			Task continueTask = responseTask.ContinueWith((x) =>
			{
				string body = new StreamReader(x.Result.GetResponseStream()).ReadToEnd();
				User user = JsonConvert.DeserializeObject<User>(body);
				SetProperties(user);
				Filmfy.Login.LoggedInUser = this;
			}, TaskScheduler.FromCurrentSynchronizationContext());

			await continueTask;

		}

		public void SetProperties(User user)
		{
			this.ID = user.ID;
			this.Username = user.Username;
			this.Password = user.Password;
			this.Email = user.Email;
			this.Birthday = user.Birthday;
			this.Type = user.Type;
			this.LoggedIn = user.LoggedIn;
			this.Favourites = user.Favourites;
		}

		public async void Register()
		{
			using (var client = new HttpClient())
			{

				HttpContent content = new StringContent(JsonConvert.SerializeObject(this), Encoding.UTF8, "application/json");

				Task<HttpResponseMessage> responseTask = client.PostAsync(ApiHelper.GetUrl("User/Save"), content);
				Task continueTask = responseTask.ContinueWith((x) =>
				{
					// Continue here with the code you want to do after the request is done!
				});

				await continueTask;
			}

		}

		public async void AddFavourite(int movieID)
		{
			using (var client = new HttpClient())
			{
				FavouriteModel favouriteModel = new FavouriteModel { UserID = this.ID, MovieID = movieID };

				HttpContent content = new StringContent(JsonConvert.SerializeObject(favouriteModel), Encoding.UTF8, "application/json");

				Task<HttpResponseMessage> responseTask = client.PostAsync(ApiHelper.GetUrl("Favourite/Add"), content);
				Task continueTask = responseTask.ContinueWith((x) =>
				{
					// Add the movie to the list
					Movie movie = MainPage.Movies.Where(m => m.ID == movieID).FirstOrDefault();
					if (movie != null && !this.Favourites.Contains(movie))
						this.Favourites.Add(movie);
				}, TaskScheduler.FromCurrentSynchronizationContext());

				await continueTask;
			}
		}

		public async void DeleteFavourite(int movieID)
		{
			using (var client = new HttpClient())
			{
				FavouriteModel favouriteModel = new FavouriteModel { UserID = this.ID, MovieID = movieID };

				HttpContent content = new StringContent(JsonConvert.SerializeObject(favouriteModel), Encoding.UTF8, "application/json");

				Task<HttpResponseMessage> responseTask = client.PostAsync(ApiHelper.GetUrl("Favourite/Delete"), content);
				Task continueTask = responseTask.ContinueWith((x) =>
				{
					// Remove the movie from the list
					Movie movie = MainPage.Movies.Where(m => m.ID == movieID).FirstOrDefault();
					if (movie != null && this.Favourites.Contains(movie))
						this.Favourites.Remove(movie);
				}, TaskScheduler.FromCurrentSynchronizationContext());

				await continueTask;
			}
		}
	}
}
