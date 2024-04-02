namespace ECommerceApi.Application.Bases
{
    public class ResponseHandler
    {
        public ResponseHandler()
        {

        }
        public static Response<T> Deleted<T>(string Message)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = Message ?? "Deleted"
            };
        }
        public static Response<T> Success<T>(T entity, object Meta)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = "Success",
                Meta = Meta
            };
        }
        public static Response<T> Unauthorized<T>(string Message)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = Message ?? "UnAuthorized"
            };
        }
        public static Response<T> BadRequest<T>(string Message)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message ?? "BadRequest"
            };
        }

        public static Response<T> UnprocessableEntity<T>(string Message)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = Message ?? "UnprocessableEntity"
            };
        }


        public static Response<T> NotFound<T>(string Message)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = Message ?? "NotFound"
            };
        }

        public static Response<T> Created<T>(T entity, object Meta)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = "Created",
                Meta = Meta
            };
        }
    }
}
