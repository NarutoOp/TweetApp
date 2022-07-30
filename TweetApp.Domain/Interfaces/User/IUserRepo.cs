namespace TweetApp.Domain.Interfaces.User
{
    using TweetApp.Domain.Models.Users;

    /// <summary>
    /// IUserRepository interface
    /// </summary>
    public interface IUserRepo
    {
        /// <summary>
        /// Get all users from database
        /// </summary>
        /// <returns></returns>
        List<User> GetAllUser();

        /// <summary>
        /// GetUser by Id from database
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>User instance</returns>
        User GetUser(string id);

        /// <summary>
        /// GetUser by Username from database
        /// </summary>
        /// <param name="username">username partial or full</param>
        /// <returns>User list</returns>
        List<User> GetUserByUsername(string username);

        /// <summary>
        /// Add user into the database
        /// </summary>
        /// <returns></returns>
        User AddUser(User user);

        /// <summary>
        /// Update user by id from database
        /// </summary>
        /// <returns></returns>
        User UpdateUser(User user);

        /// <summary>
        /// Remove user by id from database
        /// </summary>
        /// <returns></returns>
        void RemoveUser(string id);


    }
}
