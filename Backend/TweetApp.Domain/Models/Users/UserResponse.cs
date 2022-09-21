namespace TweetApp.Domain.Models.Users
{
    public class UserResponse
    {
        /// <summary>
        /// Id property get or sets
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// UserName property get or sets
        /// </summary>
        public string UserName { get; set; }

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
        /// ContactNumber property get or sets
        /// </summary>
        public string ContactNumber { get; set; }
    }
}
