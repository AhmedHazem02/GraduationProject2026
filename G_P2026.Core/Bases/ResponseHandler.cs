using System.Net;

namespace G_P2026.Core.Bases
{
	public class ResponseHandler
	{
		public Response<T> Deleted<T>(string message = null)
		{
			return new Response<T>()
			{
				StatusCode = HttpStatusCode.OK,
				Succeeded = true,
				Message = message ?? "Deleted Successfully"
			};
		}

		public Response<T> Success<T>(T entity, object meta = null)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = HttpStatusCode.OK,
				Succeeded = true,
				Message = "Success",
				Meta = meta
			};
		}

		public Response<T> Unauthorized<T>(string message = null)
		{
			return new Response<T>()
			{
				StatusCode = HttpStatusCode.Unauthorized,
				Succeeded = false,
				Message = message ?? "Unauthorized"
			};
		}

		public Response<T> BadRequest<T>(string message = null)
		{
			return new Response<T>()
			{
				StatusCode = HttpStatusCode.BadRequest,
				Succeeded = false,
				Message = message ?? "Bad Request"
			};
		}

		public Response<T> UnprocessableEntity<T>(string message = null)
		{
			return new Response<T>()
			{
				StatusCode = HttpStatusCode.UnprocessableEntity,
				Succeeded = false,
				Message = message ?? "Unprocessable Entity"
			};
		}

		public Response<T> NotFound<T>(string message = null)
		{
			return new Response<T>()
			{
				StatusCode = HttpStatusCode.NotFound,
				Succeeded = false,
				Message = message ?? "Not Found"
			};
		}

		public Response<T> Created<T>(T entity, object meta = null)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = HttpStatusCode.Created,
				Succeeded = true,
				Message = "Created Successfully",
				Meta = meta
			};
		}
	}
}
