using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmfy.Models
{
	public class BaseModel
	{
		/// <summary>
		/// The ID
		/// </summary>
		public int ID { get; set; }

		public BaseModel() { }

		public BaseModel(int id)
		{
			this.ID = id;
		}
	}
}
