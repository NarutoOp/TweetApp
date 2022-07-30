namespace TweetApp.DAL.Models.Tweet
{
    using MongoDB.Bson.Serialization.Attributes;

    public class TweetMessageDTO
    {
        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }

        [BsonElement("created")]
        public DateTime Created { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("tag")]
        public List<string>? Tag { set; get; }
    }
}
