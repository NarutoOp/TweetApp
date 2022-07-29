namespace TweetApp.Domain.Interfaces.User
{
    using TweetApp.Domain.Models.Users;

    /// <summary>
    /// IRegeisterUserService Interface
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Add user into the database
        /// </summary>
        /// <returns></returns>
        User RegisterUser(User user);

        /// <summary>
        /// Add user into the database
        /// </summary>
        /// <returns></returns>
        bool Login(UserLogin loginDetails);

        /// <summary>
        /// Update user into the database
        /// </summary>
        /// <returns></returns>
        User UpdatePassword(UserLogin userLogin);
    }
}
