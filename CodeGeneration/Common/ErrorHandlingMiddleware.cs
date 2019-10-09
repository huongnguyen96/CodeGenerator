using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Common
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int code = (int)HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is MessageException)
                code = 420;

            string result = exception.Message;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            return context.Response.WriteAsync(result);
        }
    }

    public class MessageException : Exception
    {
        public object obj { get; }
        private static string ModifyMessage(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public MessageException(object obj) : base(ModifyMessage(obj))
        {
            this.obj = obj;
        }
    }
}