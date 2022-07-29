namespace TweetApp.DAL.Translator
{
    using TweetApp.DAL.Models.Tweet;
    using TweetApp.Domain.Models.Tweet;

    public static class TweetMessageTranslator
    {
        public static TweetMessageDTO TweetMessageToDTO(TweetMessage tweetMessage)
        {
            if (tweetMessage == null)
            {
                return null;
            }

            var tweetMessageDTO = new TweetMessageDTO
            {
                Username = tweetMessage.Username,
                Message = tweetMessage.Message,
                Created = tweetMessage.Created,
                Tag = tweetMessage.Tag
            };

            return tweetMessageDTO;
        }

        public static TweetMessage DtoToTweetMessage(TweetMessageDTO tweetMessageDTO)
        {
            if (tweetMessageDTO == null)
            {
                return null;
            }

            var tweetMessage = new TweetMessage
            {
                Username = tweetMessageDTO.Username,
                Message = tweetMessageDTO.Message,
                Created = tweetMessageDTO.Created,
                Tag = tweetMessageDTO.Tag
            };

            return tweetMessage;
        }
    }
}
