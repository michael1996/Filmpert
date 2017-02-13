using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmfy.Helpers
{
	public static class ApiHelper
	{
		/// <summary>
		/// Gets the URL.
		/// </summary>
		/// <param name="method">Controller/Method/Parameter</param>
		/// <returns></returns>
		public static string GetUrl(string method)
		{
			return string.Format("http://localhost/FilmfyApi/api/{0}", method);
		}
	}
}
