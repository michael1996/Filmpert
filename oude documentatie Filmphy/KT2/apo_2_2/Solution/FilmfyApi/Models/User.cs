using FilmfyApi.Attributes;
using FilmfyApi.Helpers;
using FilmfyLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FilmfyApi.Models
{
	/// <summary>
	/// User class
	/// </summary>
	/// <seealso cref="FilmfyApi.Models.BaseModel" />
	/// <seealso cref="FilmfyLibrary.IBaseUser" />
	[TableName("tbl_User")]
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
		public List<IBaseMovie> Favourites { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class.
		/// </summary>
		public User() : base()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Movie"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		public User(int id) : base(id)
		{ }

		/// <summary>
		/// Logs the User in.
		/// </summary>
		/// <exception cref="System.Exception">Error while logging in!</exception>
		public void Login()
		{
			string query = "Select * From tbl_User Where Username = @Username And Password = @Password";
			using (SqlCommand command = new SqlCommand(query, SqlHelper.GetConnection()))
			{
				command.Parameters.AddWithValue("@Username", this.Username);
				command.Parameters.AddWithValue("@Password", this.Password);

				try
				{
					command.Connection.Open();
					using (SqlDataReader dataReader = command.ExecuteReader())
					{
						while (dataReader.Read())
						{
							this.ID = (int) dataReader["ID"];
							this.Load();
						}
					}
					command.Connection.Close();
				}
				catch (Exception ex)
				{
					throw new Exception("Error while logging in!", ex);
				}
			}

			this.LoggedIn = true;
			this.Save();
		}

		/// <summary>
		/// Logs the User out.
		/// </summary>
		public void Logout()
		{
			this.LoggedIn = false;
			this.Save();
		}

		/// <summary>
		/// Registers the User.
		/// </summary>
		public void Register()
		{
			Save();
		}

		/// <summary>
		/// Adds a favourite.
		/// </summary>
		/// <param name="movieID">The movie identifier.</param>
		public void AddFavourite(int movieID)
		{
			Movie m = new Movie(movieID);

			if (m.ID == 0 || this.Favourites.Cast<Movie>().Where(x => x.ID == movieID).FirstOrDefault() != null)
				return;

			this.Favourites.Add(m);

			this.Save();
		}

		/// <summary>
		/// Deletes a favourite.
		/// </summary>
		/// <param name="movieID">The movie identifier.</param>
		/// <exception cref="System.Exception">Error deleting favourites!</exception>
		public void DeleteFavourite(int movieID)
		{
			Movie m = Favourites.Cast<Movie>().Where(x => x.ID == movieID).FirstOrDefault();

			// Check if we have it in the list
			if (m == null)
				return;

			// Remove the references from the link table
			string query = "DELETE FROM tbl_UserFavourite WHERE UserID = @UserID AND MovieID = @MovieID";
			using (SqlCommand command = new SqlCommand(query, SqlHelper.GetConnection()))
			{
				command.Parameters.AddWithValue("@UserID", this.ID);
				command.Parameters.AddWithValue("@MovieID", m.ID);

				try
				{
					command.Connection.Open();
					command.ExecuteNonQuery();
					command.Connection.Close();
				}
				catch (Exception ex)
				{
					throw new Exception("Error deleting favourites!", ex);
				}
			}

			// After executing the query, remove it from the list
			Favourites.Remove(m);
		}

		/// <summary>
		/// Loads the others.
		/// </summary>
		/// <exception cref="System.Exception">Error loading others!</exception>
		protected override void LoadOthers()
		{
			base.LoadOthers();
			Favourites = new List<IBaseMovie>();
			// Get all the IDs of the movies
			string query = "SELECT MovieID FROM tbl_UserFavourite WHERE UserID = @UserID;";
			List<string> queriesToExecute = new List<string>();
			using (SqlCommand command = new SqlCommand(query, SqlHelper.GetConnection()))
			{
				command.Parameters.AddWithValue("@UserID", this.ID);

				try
				{
					command.Connection.Open();
					using (SqlDataReader dataReader = command.ExecuteReader())
					{
						while (dataReader.Read())
						{
							Movie movie = new Movie((int) dataReader["MovieID"]);
							Favourites.Add(movie);
						}
					}
					command.Connection.Close();
				}
				catch (Exception ex)
				{
					throw new Exception("Error loading others!", ex);
				}
			}
		}

		/// <summary>
		/// Saves the others.
		/// </summary>
		/// <exception cref="System.Exception">Error saving others!</exception>
		protected override void SaveOthers()
		{
			base.SaveOthers();
			RemoveFavourites();

			string query = "INSERT INTO tbl_UserFavourite VALUES(@UserID, @MovieID);";
			List<string> queriesToExecute = new List<string>();
			using (SqlCommand command = new SqlCommand(query, SqlHelper.GetConnection()))
			{
				try
				{
					command.Connection.Open();
					foreach (Movie m in this.Favourites)
					{
						command.Parameters.Clear();
						command.Parameters.AddWithValue("@UserID", this.ID);
						command.Parameters.AddWithValue("@MovieID", m.ID);
						command.ExecuteNonQuery();
					}
					command.Connection.Close();
				}
				catch (Exception ex)
				{
					throw new Exception("Error saving others!", ex);
				}
			}
		}

		/// <summary>
		/// Deletes the others.
		/// </summary>
		protected override void DeleteOthers()
		{
			base.DeleteOthers();

			RemoveFavourites();

			Favourites = new List<IBaseMovie>();
		}

		/// <summary>
		/// Removes the favourites.
		/// </summary>
		/// <exception cref="System.Exception">Error deleting favourites!</exception>
		protected virtual void RemoveFavourites()
		{
			// Remove the references from the link table
			string query = "DELETE FROM tbl_UserFavourite WHERE UserID = @UserID";
			using (SqlCommand command = new SqlCommand(query, SqlHelper.GetConnection()))
			{
				command.Parameters.AddWithValue("@UserID", this.ID);

				try
				{
					command.Connection.Open();
					command.ExecuteNonQuery();
					command.Connection.Close();
				}
				catch (Exception ex)
				{
					throw new Exception("Error deleting favourites!", ex);
				}
			}
		}
	}
}