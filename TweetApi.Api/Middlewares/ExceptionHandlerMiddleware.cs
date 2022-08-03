namespace TweetApi.Api.Middlewares
{
    using TweetApp.Domain.Exceptions;
    using System.Net;
    using Newtonsoft.Json;
    using TweetApp.Domain.Models.ExceptionModels;

    /// <summary>
    /// ExceptionHandlerMiddleware class
    /// </summary>
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            try
            {
                await next(context);
            }
            catch (DomainException e)
            {
                context.Response.StatusCode = (int)e.HttpStatusCode;
                var info = new ErrorInfo(e.Message, e.Errors);
                context.Response.ContentType = "application/json";
                var json = JsonConvert.SerializeObject(info, settings);
                await context.Response.WriteAsync(json);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var info = new ErrorInfo(e.Message);
                context.Response.ContentType = "application/json";
                var json = JsonConvert.SerializeObject(info, settings);
                await context.Response.WriteAsync(json);
            };
        }
    }
}
