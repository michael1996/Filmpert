using System;
using System.Collections.Generic;

namespace FilmfyLibrary
{
	/// <summary>
	/// Base interface for the User Class
	/// </summary>
	public interface IBaseUser
	{
		/// <summary>
		/// Username of the User
		/// </summary>
		string Username { get; set; }

		/// <summary>
		/// The password the user uses to login
		/// </summary>
		string Password { get; set; }

		/// <summary>
		/// Email of the user
		/// </summary>
		string Email { get; set; }

		/// <summary>
		/// Date of birth of the User
		/// </summary>
		DateTime Birthday { get; set; }

		/// <summary>
		/// What kind of the user is.
		/// </summary>
		UserType Type { get; set; }

		/// <summary>
		/// Logged in or not
		/// </summary>
		bool LoggedIn { get; set; }

		/// <summary>
		/// List of favourites of the User
		/// </summary>
		List<IBaseMovie> Favourites { get; set; }

		/// <summary>
		/// Logs the User in.
		/// </summary>
		void Login();

		/// <summary>
		/// Logs the User out.
		/// </summary>
		void Logout();

        void Register();

		/// <summary>
		/// Adds a favourite.
		/// </summary>
		/// <param name="movieID">The movie identifier.</param>
		void AddFavourite(int movieID);

		/// <summary>
		/// Deletes a favourite.
		/// </summary>
		/// <param name="movieID">The movie identifier.</param>
		void DeleteFavourite(int movieID);
	}
}
