using Microsoft.Extensions.Localization;
using School.Data.Resourses;

namespace School.Core.Bases
{

    public class ResponseHandler
    {
        private readonly IStringLocalizer<SharedResourses> _localizer;
        public ResponseHandler(IStringLocalizer<SharedResourses> localizer)
        {
            _localizer = localizer;
        }
        public Response<T> Deleted<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = "Deleted Successfully"
            };
        }
        public Response<T> Success<T>(T entity, object Meta = null, string message = "Successfully")
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = message,
                Meta = Meta
            };
        }
        public Response<T> Unauthorized<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = "UnAuthorized"
            };
        }
        public Response<T> BadRequest<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? "Bad Request" : Message
            };
        }

        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? _localizer[SharedResourseKeys.NotFound] : message
            };
        }
        public Response<T> Created<T>(T entity, object Meta = null, string message = "Successfully")
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = message,
                Meta = Meta
            };
        }
    }



}
