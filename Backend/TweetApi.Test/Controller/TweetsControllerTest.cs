namespace TweetApi.Tests.Controller
{
    using AutoFixture;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using System.Net;
    using TweetApi.Api.Controllers;
    using TweetApp.Domain.Exceptions;
    using TweetApp.Domain.Interfaces.Tweet;
    using TweetApp.Domain.Models.Tweet;

    public class TweetsControllerTest
    {
        private Mock<ITweetService> _mockTweetService;
        private IFixture _fixture;
        private TweetsController _tweetController;
        private Mock<ILogger<TweetsController>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockTweetService = new Mock<ITweetService>();
            _mockLogger = new Mock<ILogger<TweetsController>>();
            _tweetController = new TweetsController(_mockTweetService.Object, _mockLogger.Object);
        }

        [Test]
        public void GetUserTweets_ValidResponse()
        {
            var TweetList = _fixture
                            .CreateMany<Tweet>(2)
                            .ToList();
            _mockTweetService.Setup(x => x.GetUserTweets(It.IsAny<string>())).Returns(TweetList);
            var ActualResult = _tweetController.GetUserTweets("test");
            Assert.IsNotNull(ActualResult);
            Assert.IsInstanceOf<OkObjectResult>(ActualResult);
        }

        [Test]
        public void GetAllTweets_ValidResponse()
        {
            var TweetList = _fixture
                            .CreateMany<Tweet>(2)
                            .ToList();
            _mockTweetService.Setup(x => x.GetAllTweets()).Returns(TweetList);
            var ActualResult = _tweetController.GetAllTweets();
            Assert.IsNotNull(ActualResult);
            Assert.IsInstanceOf<OkObjectResult>(ActualResult);
        }

        [Test]
        public void AddTweet_ValidResponse()
        {
            var tweet = _fixture.Create<Tweet>();
            _mockTweetService.Setup(x => x.AddTweet(It.IsAny<string>(), It.IsAny<Tweet>())).Returns(tweet);
            var ActualResult = _tweetController.AddTweet("test", tweet);
            Assert.IsNotNull(ActualResult);
            Assert.IsInstanceOf<OkObjectResult>(ActualResult);
        }

        [Test]
        public void AddTweet_ShouldThrow_BadRequestException()
        {
            var exception = Assert.Throws<DomainException>(() => _tweetController.AddTweet("", null));
            Assert.AreEqual(exception.HttpStatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(exception.Message, "Invalid Request");
        }

        [Test]
        public void UpdateTweet_ValidResponse()
        {
            var tweet = _fixture.Create<Tweet>();
            _mockTweetService.Setup(x => x.UpdateTweet(It.IsAny<string>(), It.IsAny<Tweet>())).Returns(tweet);
            var ActualResult = _tweetController.UpdateTweet("test","1", tweet);
            Assert.IsNotNull(ActualResult);
            Assert.IsInstanceOf<OkObjectResult>(ActualResult);
        }

        [Test]
        public void UpdateTweet_ShouldThrow_BadRequestException()
        {
            var exception = Assert.Throws<DomainException>(() => _tweetController.UpdateTweet("", "", null));
            Assert.AreEqual(exception.HttpStatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(exception.Message, "Invalid Request");
        }

        [Test]
        public void LikeTweet_ValidResponse()
        {
            var tweet = _fixture.Create<Tweet>();
            _mockTweetService.Setup(x => x.LikeTweet(It.IsAny<string>())).Returns(tweet);
            var ActualResult = _tweetController.LikeTweet("test");
            Assert.IsNotNull(ActualResult);
            Assert.IsInstanceOf<OkObjectResult>(ActualResult);
        }

        [Test]
        public void ReplyTweet_ValidResponse()
        {
            var tweet = _fixture.Create<Tweet>();
            var tweetMessage = _fixture.Create<TweetMessage>();
            _mockTweetService.Setup(x => x.ReplyTweet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TweetMessage>())).Returns(tweet);
            var ActualResult = _tweetController.ReplyTweet("test", "1", tweetMessage);
            Assert.IsNotNull(ActualResult);
            Assert.IsInstanceOf<OkObjectResult>(ActualResult);
        }


        [Test]
        public void DeleteTweet_ValidResponse()
        {
            _mockTweetService.Setup(x => x.DeleteTweet(It.IsAny<string>())).Returns(1);
            var ActualResult = _tweetController.DeleteTweet("1");
            Assert.IsNotNull(ActualResult);
            Assert.IsInstanceOf<OkObjectResult>(ActualResult);
        }
    }
}
