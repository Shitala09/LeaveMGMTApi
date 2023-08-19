using System.Net;

namespace LeaveMGMTApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsyn(context, ex);
            }
        }

        private Task HandleExceptionAsyn(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                ErrorCode = context.Response.StatusCode,
                Message = ex.Message,
                StackTrace = ex.StackTrace.ToString()
            }.ToString()); ;
        }
    }
}
