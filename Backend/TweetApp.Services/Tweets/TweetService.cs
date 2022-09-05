namespace TweetApp.Services.Tweets
{
    using System.Text.RegularExpressions;
    using TweetApp.Domain;
    using TweetApp.Domain.Interfaces.Tweet;
    using TweetApp.Domain.Models.Tweet;
    using TweetApp.Domain.Validators;

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
            var response = _tweetRepo.GetAllTweets().OrderByDescending(x => x.TweetMessage.Created).ToList();
            return response;
        }

        public Tweet AddTweet(string username, Tweet tweet)
        {
            Validations.EnsureValid(tweet, new TweetValidator(tweet));
            tweet.Id = DateTime.Now.ToString("yyyyMMddHHmmss");
            tweet.TweetMessage.Username = username;
            tweet.TweetMessage.Created = DateTime.Now;
            tweet.TweetMessage.Tag = FetchTags(tweet.TweetMessage.Message);
            return _tweetRepo.AddTweet(tweet);
        }

        public Tweet UpdateTweet(string id, Tweet tweet)
        {
            FieldValidator.IsValidId(id);
            Validations.EnsureValid(tweet, new TweetValidator(tweet));
            tweet.TweetMessage.Created = DateTime.Now;
            tweet.TweetMessage.Tag = FetchTags(tweet.TweetMessage.Message);
            return _tweetRepo.UpdateTweet(id, tweet);
        }

        public LikeTweetResponse LikeTweet(string username, string id)
        {
            FieldValidator.IsValidId(id);
            return _tweetRepo.LikeTweet(username, id);
        }

        public Tweet ReplyTweet(string username, string id, TweetMessage message)
        {
            FieldValidator.IsValidId(id);
            Validations.EnsureValid(message, new TweetMessageValidator(message));
            message.Username = username;
            message.Created = DateTime.Now;
            message.Tag = FetchTags(message.Message);
            return _tweetRepo.ReplyTweet(id, message);
        }

        public long DeleteTweet(string id)
        {
            FieldValidator.IsValidId(id);
            return _tweetRepo.RemoveTweet(id);
        }

        private List<string> FetchTags(string s)
        {
            List<string> tags = new List<string>();
            foreach(string l in s.Split(" ")){
                if(l.StartsWith('#') & !tags.Exists(x => x == l) & l.Length <= 50)
                {
                    tags.Add(l);
                }
            }
            return tags;
        }
    }
}
