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
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

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
                _logger.LogInformation("Domain Exception - {message}{errorMessage}{httpStatusCode}{source}{stackTrace}{targetSite}{status}", e.Message, e.ErrorMessage, (int)e.HttpStatusCode, e.Source, e.StackTrace, e.TargetSite, "fail");
            }
            catch (Exception e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var info = new ErrorInfo(e.Message);
                context.Response.ContentType = "application/json";
                var json = JsonConvert.SerializeObject(info, settings);
                await context.Response.WriteAsync(json);
                _logger.LogInformation("Exception - {message}{httpStatusCode}{source}{stackTrace}{targetSite}{status}", e.Message, 500, e.Source, e.StackTrace, e.TargetSite, "fail");
            };
        }
    }
}
