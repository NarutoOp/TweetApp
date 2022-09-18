namespace TweetApi.Api
{
    using MongoDB.Driver;
    using TweetApi.Api.Middlewares;
    using TweetApp.Api.Extensions;
    using Serilog;
    using Serilog.Exceptions;
    using Serilog.Sinks.Elasticsearch;
    using System.Reflection;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.Filters;
    using System.Text;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add services to the container.
            services.AddCors();
            services.AddSingleton<IMongoClient>(s => new MongoClient(Configuration.GetValue<string>("DatabaseSettings:ConnectionString")));
            services.AddMyDependencyGroup();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(Configuration.GetSection("Jwt:Key").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
            });
        }

        //#region helper
        //    void ConfigureLogs()
        //    {
        //        // Get the environment which the application is running on
        //        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        //        // Get the configuration 
        //        var configuration = new ConfigurationBuilder()
        //                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //                .Build();

        //        // Create Logger
        //        Log.Logger = new LoggerConfiguration()
        //            .Enrich.FromLogContext()
        //            .Enrich.WithExceptionDetails() // Adds details exception
        //            .WriteTo.Debug()
        //            .WriteTo.Console()
        //            .WriteTo.Elasticsearch(ConfigureELS(configuration, env))
        //            .CreateLogger();
        //    }

        //    ElasticsearchSinkOptions ConfigureELS(IConfigurationRoot configuration, string env)
        //    {
        //        return new ElasticsearchSinkOptions(new Uri(configuration["ELKConfiguration:Uri"]))
        //        {
        //            AutoRegisterTemplate = true,
        //            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{env.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
        //        };
        //    }
        //    #endregion
    }
}