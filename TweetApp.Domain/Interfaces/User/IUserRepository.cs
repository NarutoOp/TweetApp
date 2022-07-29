namespace TweetApp.Domain.Interfaces.User
{
    using TweetApp.Domain.Models.Users;

    /// <summary>
    /// IUserRepository interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get all users from database
        /// </summary>
        /// <returns></returns>
        List<User> Get();

        /// <summary>
        /// GetUser by Id from database
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="isEmailId">To check whether id passed is email or username</param>
        /// <returns>User instance</returns>
        User Get(string id, bool isEmailId);

        /// <summary>
        /// Add user into the database
        /// </summary>
        /// <returns></returns>
        User Create(User user);

        /// <summary>
        /// Update user by id from database
        /// </summary>
        /// <returns></returns>
        User Update(User user);

        /// <summary>
        /// Remove user by id from database
        /// </summary>
        /// <returns></returns>
        void Remove(string id);


    }
}
