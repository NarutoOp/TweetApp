namespace TweetApp.Domain.Interfaces.Tweet
{
    using TweetApp.Domain.Models.Tweet;

    public interface ITweetRepo
    {
        List<Tweet> GetTweetByUsername(string username);
        List<Tweet> GetAllTweets();
        Tweet AddTweet(Tweet tweet);
        Tweet UpdateTweet(string id, Tweet tweet);
        Tweet LikeTweet(string id);
        Tweet ReplyTweet(string id, TweetMessage message);
        long RemoveTweet(string id);

    }
}