using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace FilmfyApi
{
	public class BaseApiController : ApiController
	{
		/// <summary>
		/// Jsons the response.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <param name="statusCode">The status code.</param>
		/// <returns>Response Message with Json Serialized content and the statusCode specified</returns>
		public HttpResponseMessage JsonResponse(object content, HttpStatusCode statusCode = HttpStatusCode.OK)
		{
			return new HttpResponseMessage {
				Content = new ObjectContent(content.GetType(), content, new JsonMediaTypeFormatter()),
				StatusCode = statusCode
			};
		}
	}
}