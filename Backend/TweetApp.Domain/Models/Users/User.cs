namespace TweetApp.Domain.Models.Users
{
    /// <summary>
    /// Registration Class
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id property get or sets
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// FirstName property get or sets
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName property get or sets
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email property get or sets
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Login Id property get or sets
        /// </summary>
        public string LoginId { get; set; }

        /// <summary>
        /// Password property get or sets
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// ConfirmPassword property get or sets
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// ContactNumber property get or sets
        /// </summary>
        public string ContactNumber { get; set; }
    }
}
