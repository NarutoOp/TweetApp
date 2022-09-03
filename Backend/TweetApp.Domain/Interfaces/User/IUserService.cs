namespace TweetApp.Domain.Interfaces.User
{
    using TweetApp.Domain.Models.Users;

    /// <summary>
    /// IRegeisterUserService Interface
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// GetAllUsers method
        /// </summary>
        /// <returns>List of users</returns>
        List<User> GetAllUsers();

        /// <summary>
        /// GetUserByUsername method
        /// </summary>
        /// <returns>List of users</returns>
        List<User> GetUserByUsername(string username);

        /// <summary>
        /// Add user into the database
        /// </summary>
        /// <returns></returns>
        UserResponse RegisterUser(User user);

        /// <summary>
        /// Add user into the database
        /// </summary>
        /// <returns></returns>
        LoginResponse Login(UserLogin loginDetails);

        /// <summary>
        /// Update user into the database
        /// </summary>
        /// <returns></returns>
        User UpdatePassword(UserLogin userLogin);
    }
}
