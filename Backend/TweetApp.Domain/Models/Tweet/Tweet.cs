namespace TweetApp.Domain.Models.Tweet
{
    public class Tweet
    {
        public string? Id { get; set; }
        public TweetMessage TweetMessage { get; set; }
        public int? Like { get; set; }
        public List<TweetMessage>? Reply { set; get; }
    }
}
