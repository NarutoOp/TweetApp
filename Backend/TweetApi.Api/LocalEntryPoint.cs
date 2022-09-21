namespace TweetApi.Api
{
    using AWS.Logger.SeriLog;
    using Serilog;
    using Serilog.Formatting.Compact;

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
                .UseSerilog((ctx, lc) =>
                {
                    lc
                        .ReadFrom.Configuration(ctx.Configuration)
                        .WriteTo.AWSSeriLog(
                            configuration: ctx.Configuration,
                            textFormatter: new RenderedCompactJsonFormatter());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}