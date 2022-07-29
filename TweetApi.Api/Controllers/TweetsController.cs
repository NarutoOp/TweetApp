namespace TweetApi.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TweetApp.Domain.Interfaces.Tweet;
    using TweetApp.Domain.Models.Tweet;

    /// <summary>
    /// TweetsController Class
    /// </summary>
    [Route("api/v1.0/tweets")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        /// <summary>
        /// ITweetService instance
        /// </summary>
        private readonly ITweetService _tweetService;

        /// <summary>
        /// TweetsController constructor
        /// </summary>
        public TweetsController(ITweetService tweetService)
        {
            _tweetService = tweetService;
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
            var result = _tweetService.AddTweet(username, tweet);
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
            var result = _tweetService.UpdateTweet(id, tweet);
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
            return Ok(result);
        }

        /// <summary>
        /// ReplyTweets Controller
        /// </summary>
        /// <returns></returns>
        [Route("{username}/reply/{id}")]
        [HttpPut]
        public ActionResult ReplyTweet(string id, [FromBody] string message)
        {
            var result = _tweetService.ReplyTweet(id, message);
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
            _tweetService.DeleteTweet(id);
            return Ok("Deleted");
        }
    }
}
