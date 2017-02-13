using FilmfyApi.Models;
using FilmfyLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace FilmfyApi.Controllers
{
	/// <summary>
	/// Controller that manages everything for the Movies
	/// </summary>
	/// <seealso cref="FilmfyApi.BaseApiController" />
	/// <seealso cref="FilmfyApi.Models.Movie"/>
	public class MovieController : BaseApiController
	{
		/// <summary>
		/// Gets the Movie using the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		[HttpGet]
		public HttpResponseMessage Get(int id)
		{
			Movie m = new Movie(id);
			if (m.ID == 0)
			{
				// Return server error
				return this.JsonResponse("ID not found", HttpStatusCode.NotFound);
			}
			
			return this.JsonResponse(m);
		}

		/// <summary>
		/// Gets all movies.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public HttpResponseMessage GetAll()
		{
			return JsonResponse(BaseModel.GetAll<Movie>());
		}
	}
}
