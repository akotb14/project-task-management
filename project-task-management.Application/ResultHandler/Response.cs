using System.Net;

namespace project_task_management.Application.ResultHandler
{
    public class Response<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Succeeded { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Errors { get; set; }
        public object Meta { get; set; }


        public Response()
        {

        }
        public Response(string message , HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            StatusCode = statusCode;
            Succeeded = true;
            Message = message;
        }
        public Response(T data, string message = null , HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            StatusCode = statusCode;
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(string message, bool succeeded , HttpStatusCode statusCode = HttpStatusCode.NoContent )
        {
            StatusCode = succeeded == false ? statusCode : HttpStatusCode.OK ;
            Succeeded = succeeded;
            Message = message;
        }


    }
}
