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

        [BsonElement("tweetMessage")]
        public TweetMessageDTO TweetMessage { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("like")]
        public int? Like { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("reply")]
        public List<TweetMessageDTO>? Reply { set; get; }

        
    }
}
