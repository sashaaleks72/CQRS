using WebAPI.Middlewares;

namespace WebAPI.Extensions
{
    public static class ExceptionHandlerMiddlwareExtension
    {
        public static IApplicationBuilder UseExceptionHandlers(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddlware>();
        }
    }
}
