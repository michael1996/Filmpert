using FilmfyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmfy.Models
{
	public class Actor : BaseModel, IBaseActor
	{
		/// <summary>
		/// Name of the Actor
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Birthday of the Actor
		/// </summary>
		public DateTime Birthday { get; set; }

		/// <summary>
		/// Image of the Actor.
		/// </summary>
		public string Image { get; set; }

		public Actor() : base() { }

		public Actor(int id) : base(id) { }
	}
}
