namespace TweetApi.Api;
using Microsoft.Extensions.Logging;

/// <summary>
/// The Main function can be used to run the ASP.NET Core application locally using the Kestrel webserver.
/// </summary>
public class LocalEntryPoint
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.AddAWSProvider();

                // When you need logging below set the minimum level. Otherwise the logging framework will default to Informational for external providers.
                logging.SetMinimumLevel(LogLevel.Debug);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}