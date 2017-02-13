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
	/// Controller that manages everything for the user
	/// </summary>
	/// <seealso cref="FilmfyApi.BaseApiController" />
	/// <seealso cref="FilmfyApi.Models.User"/>
	public class UserController : BaseApiController
	{
		/// <summary>
		/// Gets the user using the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		[HttpGet]
		public HttpResponseMessage Get(int id)
		{
			return JsonResponse(new User(id));
		}

		/// <summary>
		/// Gets all users.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public HttpResponseMessage GetAll()
		{
			return JsonResponse(BaseModel.GetAll<User>());
		}

		/// <summary>
		/// Saves the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		[HttpPost]
		public HttpResponseMessage Save(User user)
		{
			try
			{
				user.Register();
			}
			catch (Exception ex)
			{
				// We catch the exception for easy debugging purposes.
				// This way we can return the exception in our response
				return JsonResponse(ex, HttpStatusCode.InternalServerError);
			}

			return JsonResponse(user);
		}

		/// <summary>
		/// Logins the user in.
		/// </summary>
		/// <param name="userModel">The user model.</param>
		/// <returns></returns>
		[HttpPost]
		public HttpResponseMessage Login(UserModel userModel)
		{
			User user = new User();
			user.Username = userModel.Username;
			user.Password = userModel.Password;
			try
			{
				user.Login();
			}
			catch (Exception ex)
			{
				return JsonResponse(ex, HttpStatusCode.InternalServerError);
			}
			return JsonResponse(user);
		}

		/// <summary>
		/// Logs the specified identifier out.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		[HttpGet]
		public HttpResponseMessage Logout(int id)
		{
			User user;
			try
			{
				user = new Models.User(id);
			}
			catch (Exception ex)
			{
				return JsonResponse(ex, HttpStatusCode.InternalServerError);
			}

			user.Logout();

			return JsonResponse(user);
		}
	}
}