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
            ErrorInfo info = null;

            try
            {
                await next(context);
            }
            catch (DomainException e)
            {
                context.Response.StatusCode = (int)e.HttpStatusCode;
                info = new ErrorInfo(e.Message, e.Errors);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                info = new ErrorInfo(e.Message);
            }
            finally
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                context.Response.ContentType = "application/json";
                var json = JsonConvert.SerializeObject(info,settings);
                await context.Response.WriteAsync(json);
            };
        }
    }
}
