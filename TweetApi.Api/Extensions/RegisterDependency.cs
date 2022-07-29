namespace TweetApp.Api.Extensions
{
    using TweetApi.Api.Middlewares;
    using TweetApp.DAL.Repository;
    using TweetApp.Domain.Interfaces.User;
    using TweetApp.Services.Users;

    /// <summary>
    /// RegisterService class
    /// </summary>
    public static class RegisterDependency
    {
        /// <summary>
        /// AddService method
        /// </summary>
        /// <param name="services">IServiceCollection instance</param>
        /// <returns>IServiceCollection instance</returns>
        public static IServiceCollection AddMyDependencyGroup(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<ExceptionHandlerMiddleware>();

            return services;
        }
    }
}
