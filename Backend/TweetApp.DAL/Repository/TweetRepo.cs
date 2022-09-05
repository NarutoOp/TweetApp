namespace TweetApp.DAL.Repository
{
    using MongoDB.Driver;
    using TweetApp.DAL.Models.Tweet;
    using TweetApp.DAL.Translator;
    using TweetApp.Domain.Exceptions;
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
            if(tweetDTO == null)
            {
                throw new DomainException("Id is invalid", System.Net.HttpStatusCode.BadRequest);
            }
            tweetDTO.TweetMessage.Created = tweet.TweetMessage.Created;
            tweetDTO.TweetMessage.Message = tweet.TweetMessage.Message;
            tweetDTO.TweetMessage.Tag = tweet.TweetMessage.Tag;
            _tweetCollection.ReplaceOne(x => x.Id == id, tweetDTO);
            var result = TweetTranslator.TweetDTOToTweet(tweetDTO);
            return result;
        }

        public LikeTweetResponse LikeTweet(string username, string id)
        {
            var tweetDTO = _tweetCollection.Find(x => x.Id == id).FirstOrDefault();
            if(tweetDTO == null)
            {
                throw new DomainException("Id is invalid", System.Net.HttpStatusCode.BadRequest);
            }
            if (tweetDTO.Like == null)
            {
                tweetDTO.Like = new List<string>();
            }

            bool like;
            if(tweetDTO.Like.Any(x => string.Equals(x, username)))
            {
                tweetDTO.Like.Remove(username);
                like = false;
            }
            else
            {
                tweetDTO.Like.Add(username);
                like = true;
            }
            _tweetCollection.ReplaceOne(x => x.Id == id, tweetDTO);

            return new LikeTweetResponse 
            { 
                IsLiked=like,
                LikeCount=tweetDTO.Like.Count()
            };
        }

        public Tweet ReplyTweet(string id, TweetMessage message)
        {
            var tweetDTO = _tweetCollection.Find(x => x.Id == id).FirstOrDefault();
            if(tweetDTO == null)
            {
                throw new DomainException("Id is invalid",System.Net.HttpStatusCode.BadRequest);
            }

            if(tweetDTO.Reply == null)
            {
                tweetDTO.Reply = new List<TweetMessageDTO>();
            }
            var messageDTO = TweetMessageTranslator.TweetMessageToDTO(message);
            tweetDTO.Reply.Add(messageDTO);
            _tweetCollection.ReplaceOne(x => x.Id == id, tweetDTO);

            return TweetTranslator.TweetDTOToTweet(tweetDTO);
        }

        public long RemoveTweet(string id)
        {
            return _tweetCollection.DeleteOne(x => x.Id == id).DeletedCount;
        }
    }
}
