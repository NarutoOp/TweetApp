namespace TweetApi.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TweetApp.Domain.Exceptions;
    using TweetApp.Domain.Interfaces.Tweet;
    using TweetApp.Domain.Models.Tweet;

    /// <summary>
    /// TweetsController Class
    /// </summary>
    [Route("api/v1.0/tweets")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        private readonly ILogger<TweetsController> _logger;
        /// <summary>
        /// ITweetService instance
        /// </summary>
        private readonly ITweetService _tweetService;

        /// <summary>
        /// TweetsController constructor
        /// </summary>
        public TweetsController(ITweetService tweetService, ILogger<TweetsController> logger)
        {
            _tweetService = tweetService;
            _logger = logger;
        }

        /// <summary>
        /// GetUserTweets Controller
        /// </summary>
        /// <returns></returns>
        [Route("{username}")]
        [HttpGet]
        public ActionResult GetUserTweets(string username)
        {
            var result = _tweetService.GetUserTweets(username);
            _logger.LogInformation("GetUserTweets", result);
            return Ok(result);
        }

        /// <summary>
        /// GetAllTweets Controller
        /// </summary>
        /// <returns></returns>
        [Route("all")]
        [HttpGet]
        public ActionResult GetAllTweets()
        {
            var result = _tweetService.GetAllTweets();
            _logger.LogInformation("GetAllTweets", result);
            return Ok(result);
        }

        /// <summary>
        /// AddTweets Controller
        /// </summary>
        /// <returns></returns>
        [Route("{username}/add")]
        [HttpPost]
        public ActionResult AddTweet(string username, [FromBody] Tweet tweet)
        {
            if (tweet == null)
            {
                throw new DomainException("Invalid Request", System.Net.HttpStatusCode.BadRequest);
            }
            var result = _tweetService.AddTweet(username, tweet);
            _logger.LogInformation("AddTweet", result);
            return Ok(result);
        }

        /// <summary>
        /// UpdateTweets Controller
        /// </summary>
        /// <returns></returns>
        [Route("{username}/update/{id}")]
        [HttpPut]
        public ActionResult UpdateTweet(string username, string id, [FromBody] Tweet tweet)
        {
            if (tweet == null)
            {
                throw new DomainException("Invalid Request", System.Net.HttpStatusCode.BadRequest);
            }
            var result = _tweetService.UpdateTweet(id, tweet);
            _logger.LogInformation("UpdateTweet", result);
            return Ok(result);
        }

        /// <summary>
        /// LikeTweets Controller
        /// </summary>
        /// <returns></returns>
        [Route("{username}/like/{id}")]
        [HttpPut]
        public ActionResult LikeTweet(string id)
        {
            var result = _tweetService.LikeTweet(id);
            _logger.LogInformation("LikeTweet", result);
            return Ok(result);
        }

        /// <summary>
        /// ReplyTweets Controller
        /// </summary>
        /// <returns></returns>
        [Route("{username}/reply/{id}")]
        [HttpPost]
        public ActionResult ReplyTweet(string username, string id, [FromBody] TweetMessage message)
        {
            if (message == null)
            {
                throw new DomainException("Invalid Request", System.Net.HttpStatusCode.BadRequest);
            }
            var result = _tweetService.ReplyTweet(username, id, message);
            _logger.LogInformation("ReplyTweet", result);
            return Ok(result);
        }

        /// <summary>
        /// DeleteTweets Controller
        /// </summary>
        /// <returns></returns>
        [Route("{username}/delete/{id}")]
        [HttpDelete]
        public ActionResult DeleteTweet(string id)
        {
            _logger.LogInformation("DeletedTweet");
            return Ok(_tweetService.DeleteTweet(id));
        }
    }
}
