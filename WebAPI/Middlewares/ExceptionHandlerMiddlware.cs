using Application.Common.Exceptions;
using Application.Common.Models;

namespace WebAPI.Middlewares
{
    public class ExceptionHandlerMiddlware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddlware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext) 
        {
            try
            {
                await _next(httpContext);
            }
            catch(NotFoundException notFoundEx) 
            {
                await NotFoundExceptionHandler(httpContext, notFoundEx);
            }
            catch(BadRequestException badRequestEx)
            {
                await BadRequestExceptionHandler(httpContext, badRequestEx);
            }
            catch(Exception ex)
            {
                await ExceptionHandler(httpContext, ex);
            }
        }

        private async Task NotFoundExceptionHandler(HttpContext context, NotFoundException notFoundEx)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails(context.Response.StatusCode, notFoundEx.Message);

            await context.Response.WriteAsJsonAsync(response);
        }

        private async Task BadRequestExceptionHandler(HttpContext context, BadRequestException notFoundEx)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails(context.Response.StatusCode, notFoundEx.Message);

            await context.Response.WriteAsJsonAsync(response);
        }

        private async Task ExceptionHandler(HttpContext context, Exception notFoundEx)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails(context.Response.StatusCode, notFoundEx.Message);

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
