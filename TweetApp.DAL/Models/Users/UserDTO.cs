namespace TweetApp.DAL.Models.Users
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Registration Class
    /// </summary>
    [BsonIgnoreExtraElements]
    public class UserDTO
    {
        /// <summary>
        /// Id property get or sets
        /// </summary>
        [BsonId]
        [BsonElement("Id")]
        public string Id { get; set; }

        /// <summary>
        /// FirstName property get or sets
        /// </summary>
        [BsonElement("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName property get or sets
        /// </summary>
        [BsonElement("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Email property get or sets
        /// </summary>
        [BsonElement("email")]
        public string Email { get; set; }

        /// <summary>
        /// Login Id property get or sets
        /// </summary>
        [BsonElement("loginId")]
        public string LoginId { get; set; }

        /// <summary>
        /// Password property get or sets
        /// </summary>
        [BsonElement("password")]
        public string Password { get; set; }

        /// <summary>
        /// ContactNumber property get or sets
        /// </summary>
        [BsonElement("contactNumber")]
        public string ContactNumber { get; set; }
    }
}
