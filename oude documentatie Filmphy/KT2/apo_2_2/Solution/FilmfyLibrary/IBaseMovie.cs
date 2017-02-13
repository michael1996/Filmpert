using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmfyLibrary
{
	/// <summary>
	/// Base interface for the Movie
	/// </summary>
	public interface IBaseMovie
	{
		/// <summary>
		/// Title of the Movie
		/// </summary>
		string Title { get; set; }

		/// <summary>
		/// Description of the Movie
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Release Date of the Movie
		/// </summary>
		DateTime ReleaseDate { get; set; }

		/// <summary>
		/// Link to the Poster
		/// </summary>
		string Poster { get; set; }

		/// <summary>
		/// Link to the Trailer
		/// </summary>
		string Trailer { get; set; }

		/// <summary>
		/// Actors of the Movie
		/// </summary>
		List<IBaseActor> Actors { get; set; }
	}
}
