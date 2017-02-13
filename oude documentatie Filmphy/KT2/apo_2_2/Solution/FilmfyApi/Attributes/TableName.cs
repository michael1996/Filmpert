using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmfyApi.Attributes
{
	/// <summary>
	/// Attribute for classes to set the tablename in the database
	/// </summary>
	/// <seealso cref="System.Attribute" />
	[AttributeUsage(AttributeTargets.Class)]
	public class TableName : Attribute
	{
		public string Name { get; set; }

		public TableName(string name)
		{
			this.Name = name;
		}
	}
}