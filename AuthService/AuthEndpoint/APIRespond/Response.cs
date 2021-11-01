using Microsoft.AspNetCore.Mvc;

namespace AuthEndpoint.APIRespond
{
    public class Response<T>
    {
        public T data { get; set; }
        public bool isSuccess { get; set; }
        public string error { get; set; }
        public string message { get; set; }

        public OkObjectResult Success(T data)
        {
            return new(new ApiResponse()
            {
                error = null,
                isSuccess = true,
                data = data,
                message = "Success",
            });
        }

        public BadRequestObjectResult FailedToFind(string Message)
        {
            return new(new ApiResponse()
            {
                error = "Not Found",
                isSuccess = false,
                data = default,
                message = Message,
            });
        }

    }
    public class ApiResponse : Response<object>
    {
    }
}
