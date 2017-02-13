using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmfyLibrary
{
	/// <summary>
	/// Base interface for the Actor
	/// </summary>
	public interface IBaseActor
	{
		/// <summary>
		/// Name of the Actor
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Image of the Actor.
		/// </summary>
		string Image { get; set; }

		/// <summary>
		/// Birthday of the Actor
		/// </summary>
		DateTime Birthday { get; set; }
	}
}
