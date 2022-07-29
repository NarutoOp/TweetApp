namespace TweetApp.Domain.Models.Tweet
{
    public class Tweet
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public int? Like { get; set; }
        public List<string>? Reply { set; get; }
    }
}
