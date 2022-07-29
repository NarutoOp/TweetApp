namespace TweetApp.DAL.Models.Tweet
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements]
    public class TweetDTO
    {
        [BsonId]
        [BsonElement("Id")]
        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }

        [BsonElement("created")]
        public DateTime Created { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("like")]
        public int? Like { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("reply")]
        public List<string>? Reply { set; get; }
    }
}
