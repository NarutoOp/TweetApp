namespace TweetApp.Domain.Interfaces.Tweet
{
    using TweetApp.Domain.Models.Tweet;

    /// <summary>
    /// ITweetService class
    /// </summary>
    public interface ITweetService
    {
        List<Tweet> GetUserTweets(string username);
        List<Tweet> GetAllTweets();
        Tweet AddTweet(string username, Tweet tweet);
        Tweet UpdateTweet(string id, Tweet tweet);
        Tweet LikeTweet(string id);
        Tweet ReplyTweet(string username, string id, TweetMessage message);
        long DeleteTweet(string id);
    }
}