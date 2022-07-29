namespace TweetApp.DAL.Translator
{
    using TweetApp.DAL.Models.Tweet;
    using TweetApp.Domain.Models.Tweet;

    public static class TweetTranslator
    {
        public static TweetDTO TweetToTweetDTO(Tweet tweet)
        {
            if (tweet == null)
            {
                return null;
            }

            var tweetDTO = new TweetDTO
            {
                Id = tweet.Id,
                Username = tweet.Username,
                Message = tweet.Message,
                Created = tweet.Created,
                Like = tweet.Like,
                Reply = tweet.Reply
            };

            return tweetDTO;
        }

        public static Tweet TweetDTOToTweet(TweetDTO tweetDTO)
        {
            if (tweetDTO == null)
            {
                return null;
            }

            var tweet = new Tweet
            {
                Id = tweetDTO.Id,
                Username = tweetDTO.Username,
                Message = tweetDTO.Message,
                Created = tweetDTO.Created,
                Like = tweetDTO.Like,
                Reply = tweetDTO.Reply
            };

            return tweet;
        }
    }
}
