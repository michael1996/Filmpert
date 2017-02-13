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
	/// Actor class
	/// </summary>
	/// <seealso cref="FilmfyApi.Models.BaseModel" />
	/// <seealso cref="FilmfyLibrary.IBaseActor" />
	[TableName("tbl_Actor")]
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

		/// <summary>
		/// Initializes a new instance of the <see cref="Actor"/> class.
		/// </summary>
		public Actor() : base(){ }

		/// <summary>
		/// Initializes a new instance of the <see cref="Actor"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		public Actor(int id) : base(id)
		{
		}
	}
}