using FilmfyApi.Attributes;
using FilmfyApi.Helpers;
using FilmfyLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace FilmfyApi.Models
{
	/// <summary>
	/// The implementation of Movie
	/// </summary>
	/// <seealso cref="FilmfyLibrary.BaseMovie" />
	[TableName("tbl_Movie")]
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
		/// Actors of the Movie
		/// </summary>
		public List<IBaseActor> Actors { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Movie"/> class.
		/// </summary>
		public Movie() : base()
		{
			Actors = new List<IBaseActor>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Movie"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		public Movie(int id) : base(id)
		{ }

		/// <summary>
		/// Loads the others.
		/// </summary>
		/// <exception cref="System.Exception">Error loading others!</exception>
		protected override void LoadOthers()
		{
			base.LoadOthers();

			List<IBaseActor> loadedActors = new List<IBaseActor>();

			string query = "select ID from tbl_Actor a INNER JOIN tbl_MovieActor ma ON a.ID = ma.ActorID WHERE ma.MovieID = @MovieID";

			using (SqlCommand command = new SqlCommand(query, SqlHelper.GetConnection()))
			{
				command.Parameters.AddWithValue("@MovieID", this.ID);
				try
				{
					command.Connection.Open();
					using (SqlDataReader dataReader = command.ExecuteReader())
					{
						while (dataReader.Read())
						{
							Actor actor = new Actor((int) dataReader["ID"]);
							loadedActors.Add(actor);
						}
					}

					command.Connection.Close();
				}
				catch (Exception ex)
				{
					throw new Exception("Error loading others!", ex);
				}
			}

			Actors = loadedActors;
		}

		/// <summary>
		/// Saves the others.
		/// </summary>
		/// <exception cref="System.Exception">Error saving others!</exception>
		protected override void SaveOthers()
		{
			base.SaveOthers();

			// Deletes all the references
			DeleteOthers();

			// First save all the actors
			foreach (Actor a in this.Actors)
			{
				a.Save();
			}

			string query = "INSERT INTO tbl_MovieActor VALUES(@MovieID, @ActorID);";
			List<string> queriesToExecute = new List<string>();
			using (SqlCommand command = new SqlCommand(query, SqlHelper.GetConnection()))
			{
				try
				{
					command.Connection.Open();
					foreach (Actor a in this.Actors)
					{
						command.Parameters.Clear();
						command.Parameters.AddWithValue("@MovieID", this.ID);
						command.Parameters.AddWithValue("@ActorID", a.ID);
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
		/// <exception cref="System.Exception">Error deleting others!</exception>
		protected override void DeleteOthers()
		{
			base.DeleteOthers();

			// Remove the references from the link table
			string query = "DELETE FROM tbl_MovieActor WHERE MovieID = @MovieID";
			using (SqlCommand command = new SqlCommand(query, SqlHelper.GetConnection()))
			{
				command.Parameters.AddWithValue("@MovieID", this.ID);

				try
				{
					command.Connection.Open();
					command.ExecuteNonQuery();
					command.Connection.Close();
				}
				catch (Exception ex)
				{
					throw new Exception("Error deleting others!", ex);
				}
			}
		}
	}
}