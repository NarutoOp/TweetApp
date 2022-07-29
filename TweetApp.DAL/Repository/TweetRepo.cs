namespace TweetApp.DAL.Repository
{
    using MongoDB.Driver;
    using TweetApp.DAL.Models.Tweet;
    using TweetApp.DAL.Translator;
    using TweetApp.Domain.Interfaces.Tweet;
    using TweetApp.Domain.Models.Tweet;

    public class TweetRepo: Configuration, ITweetRepo
    {
        /// <summary>
        /// MongoCollection instance
        /// </summary>
        private readonly IMongoCollection<TweetDTO> _tweetCollection;

        /// <summary>
        /// UserRepository constructor
        /// </summary>
        /// <param name="mongoClient">IMongoClient instance</param>
        public TweetRepo(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(_databaseName);
            _tweetCollection = database.GetCollection<TweetDTO>(_tweetCollectionName);
        }

        public List<Tweet> GetTweetByUsername(string username)
        {
            var tweetDTOList = _tweetCollection.Find(x => x.TweetMessage.Username == username).ToList();
            var tweetList = tweetDTOList.ConvertAll(x => TweetTranslator.TweetDTOToTweet(x)).ToList();
            return tweetList;
        }

        public List<Tweet> GetAllTweets()
        {
            var tweetDTOList = _tweetCollection.Find(user => true).ToList();
            var tweetList = tweetDTOList.ConvertAll(x => TweetTranslator.TweetDTOToTweet(x)).ToList();
            return tweetList;
        }

        /// <summary>
        /// Add tweet into the database
        /// </summary>
        /// <param name="tweet">Tweet instance</param>
        /// <returns>Tweet instance</returns>
        public Tweet AddTweet(Tweet tweet)
        {
            var tweetDTO = TweetTranslator.TweetToTweetDTO(tweet);
            _tweetCollection.InsertOne(tweetDTO);
            return tweet;
        }

        public Tweet UpdateTweet(string id, Tweet tweet)
        {
            var tweetDTO = _tweetCollection.Find(x => x.Id == id).FirstOrDefault();
            tweetDTO.TweetMessage.Created = tweet.TweetMessage.Created;
            tweetDTO.TweetMessage.Message = tweet.TweetMessage.Message;
            _tweetCollection.ReplaceOne(x => x.Id == id, tweetDTO);
            return tweet;
        }

        public Tweet LikeTweet(string id)
        {
            var tweetDTO = _tweetCollection.Find(x => x.Id == id).FirstOrDefault();
            if (tweetDTO.Like == null)
            {
                tweetDTO.Like = 0;
            }
            tweetDTO.Like++;
            _tweetCollection.ReplaceOne(x => x.Id == id, tweetDTO);

            return TweetTranslator.TweetDTOToTweet(tweetDTO);
        }

        public Tweet ReplyTweet(string id, TweetMessage message)
        {
            var tweetDTO = _tweetCollection.Find(x => x.Id == id).FirstOrDefault();
            if(tweetDTO.Reply == null)
            {
                tweetDTO.Reply = new List<TweetMessageDTO>();
            }
            var messageDTO = TweetMessageTranslator.TweetMessageToDTO(message);
            tweetDTO.Reply.Add(messageDTO);
            _tweetCollection.ReplaceOne(x => x.Id == id, tweetDTO);

            return TweetTranslator.TweetDTOToTweet(tweetDTO);
        }

        public void RemoveTweet(string id)
        {
            _tweetCollection.DeleteOne(x => x.Id == id);
        }
    }
}
