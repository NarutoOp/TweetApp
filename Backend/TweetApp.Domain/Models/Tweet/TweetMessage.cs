namespace TweetApp.Domain.Models.Tweet
{
    public class TweetMessage
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public List<string>? Tag { set; get; }
    }
}
