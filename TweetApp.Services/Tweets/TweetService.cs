namespace TweetApp.Services.Tweets
{
    using TweetApp.Domain.Interfaces.Tweet;
    using TweetApp.Domain.Models.Tweet;

    /// <summary>
    /// TweetService class
    /// </summary>
    public class TweetService: ITweetService
    {
        /// <summary>
        /// IUserRepository instance
        /// </summary>
        private readonly ITweetRepo _tweetRepo;

        /// <summary>
        /// TweetService Class
        /// </summary>
        /// <param name="tweetRepo">ITweetRepo instance</param>
        public TweetService(ITweetRepo tweetRepo)
        {
            _tweetRepo = tweetRepo;
        }

        public List<Tweet> GetUserTweets(string username)
        {
            return _tweetRepo.GetTweetByUsername(username);
        }

        public List<Tweet> GetAllTweets()
        {
            return _tweetRepo.GetAllTweets();
        }

        public Tweet AddTweet(string username, Tweet tweet)
        {
            tweet.Id = DateTime.Now.ToString("yyyyMMddHHmmss");
            tweet.TweetMessage.Username = username;
            tweet.TweetMessage.Created = DateTime.Now;
            return _tweetRepo.AddTweet(tweet);
        }

        public Tweet UpdateTweet(string id, Tweet tweet)
        {
            tweet.TweetMessage.Created = DateTime.Now;
            return _tweetRepo.UpdateTweet(id, tweet);
        }

        public Tweet LikeTweet(string id)
        {
            return _tweetRepo.LikeTweet(id);
        }

        public Tweet ReplyTweet(string username, string id, TweetMessage message)
        {
            message.Username = username;
            return _tweetRepo.ReplyTweet(id, message);
        }

        public void DeleteTweet(string id)
        {
            _tweetRepo.RemoveTweet(id);
        }
    }
}
