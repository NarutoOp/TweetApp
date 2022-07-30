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

            var replyDTOList = tweet.Reply?.ConvertAll(x => TweetMessageTranslator.TweetMessageToDTO(x)).ToList();

            var tweetDTO = new TweetDTO
            {
                Id = tweet.Id,
                TweetMessage = TweetMessageTranslator.TweetMessageToDTO(tweet.TweetMessage),
                Like = tweet.Like,
                Reply = replyDTOList
            };

            return tweetDTO;
        }

        public static Tweet TweetDTOToTweet(TweetDTO tweetDTO)
        {
            if (tweetDTO == null)
            {
                return null;
            }

            var replyList = tweetDTO.Reply?.ConvertAll(x => TweetMessageTranslator.DtoToTweetMessage(x)).ToList();

            var tweet = new Tweet
            {
                Id = tweetDTO.Id,
                TweetMessage = TweetMessageTranslator.DtoToTweetMessage(tweetDTO.TweetMessage),
                Like = tweetDTO.Like,
                Reply = replyList
            };

            return tweet;
        }
    }
}
