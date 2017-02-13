using FilmfyApi.Models;
using FilmfyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FilmfyApi.Controllers
{
	/// <summary>
	/// Controller that manages everything for the favourites
	/// </summary>
	/// <seealso cref="FilmfyApi.BaseApiController" />
	/// <seealso cref="FilmfyApi.Models.User"/>
	/// <seealso cref="FilmfyApi.Models.Movie"/>
	public class FavouriteController : BaseApiController
	{
		/// <summary>
		/// Adds the specified favourite to the user.
		/// </summary>
		/// <param name="favouriteModel">The favourite model.</param>
		/// <returns></returns>
		[HttpPost]
		public HttpResponseMessage Add(FavouriteModel favouriteModel)
		{
			User user = new Models.User(favouriteModel.UserID);
			user.AddFavourite(favouriteModel.MovieID);

			return JsonResponse(user);
		}

		/// <summary>
		/// Deletes the specified favourite from the user.
		/// </summary>
		/// <param name="favouriteModel">The favourite model.</param>
		/// <returns></returns>
		[HttpPost]
		public HttpResponseMessage Delete(FavouriteModel favouriteModel)
		{
			User user = new Models.User(favouriteModel.UserID);
			user.DeleteFavourite(favouriteModel.MovieID);

			return JsonResponse(user);
		}
	}
}
