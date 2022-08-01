namespace TweetApi.Tests.Services
{
    using AutoFixture;
    using Moq;
    using NUnit.Framework;
    using System.Net;
    using TweetApp.Domain.Exceptions;
    using TweetApp.Domain.Interfaces.Tweet;
    using TweetApp.Domain.Models.Tweet;
    using TweetApp.Services.Tweets;

    public class TweetServiceTest
    {
        private Mock<ITweetRepo> _mockTweetRepo;
        private IFixture _fixture;
        private ITweetService _tweetService;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockTweetRepo = new Mock<ITweetRepo>();
            _tweetService = new TweetService(_mockTweetRepo.Object);
        }

        [Test]
        public void GetUserTweets_ValidResponse()
        {
            var TweetList = _fixture
                            .CreateMany<Tweet>(2)
                            .ToList();
            _mockTweetRepo.Setup(x => x.GetTweetByUsername(It.IsAny<string>())).Returns(TweetList);
            var ActualResult = _tweetService.GetUserTweets("test");
            Assert.IsNotNull(ActualResult);
            Assert.That(TweetList, Is.EqualTo(ActualResult));
        }

        [Test]
        public void GetAllTweets_ValidResponse()
        {
            var TweetList = _fixture
                            .CreateMany<Tweet>(2)
                            .ToList();
            _mockTweetRepo.Setup(x => x.GetAllTweets()).Returns(TweetList);
            var ActualResult = _tweetService.GetAllTweets();
            Assert.IsNotNull(ActualResult);
            Assert.That(TweetList, Is.EqualTo(ActualResult));
        }

        [Test]
        public void AddTweet_ValidResponse()
        {
            var tweet = _fixture
                            .Create<Tweet>();
            _mockTweetRepo.Setup(x => x.AddTweet(It.IsAny<Tweet>())).Returns(tweet);
            var ActualResult = _tweetService.AddTweet("test", tweet);
            Assert.IsNotNull(ActualResult);
            Assert.That(tweet, Is.EqualTo(ActualResult));
        }

        [Test]
        public void AddTweet_ShouldThrow_BadRequestException()
        {
            var tweet = new Tweet
            {
                TweetMessage = new TweetMessage
                {
                    Message = ""
                }
            };

            var exception = Assert.Throws<DomainException>(() => _tweetService.AddTweet("test", tweet));
            Assert.AreEqual(exception.HttpStatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(exception.Errors.FirstOrDefault().Message, "Tweet Message cannot be blank.");
        }

        [Test]
        public void UpdateTweet_ValidResponse()
        {
            var tweet = _fixture
                            .Create<Tweet>();
            _mockTweetRepo.Setup(x => x.UpdateTweet(It.IsAny<string>(),It.IsAny<Tweet>())).Returns(tweet);
            var ActualResult = _tweetService.UpdateTweet("11112233445566", tweet);
            Assert.IsNotNull(ActualResult);
            Assert.That(tweet, Is.EqualTo(ActualResult));
        }

        [Test]
        public void UpdateTweet_ShouldThrow_BadRequestException()
        {
            var tweet = _fixture.Create<Tweet>();
            var exception = Assert.Throws<DomainException>(() => _tweetService.UpdateTweet("", tweet));
            Assert.AreEqual(exception.HttpStatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(exception.Message, "Id Cannot be empty");
        }

        [Test]
        public void LikeTweet_ValidResponse()
        {
            var tweet = _fixture.Create<Tweet>();
            _mockTweetRepo.Setup(x => x.LikeTweet(It.IsAny<string>())).Returns(tweet);
            var ActualResult = _tweetService.LikeTweet("11112233445566");
            Assert.IsNotNull(ActualResult);
            Assert.That(tweet, Is.EqualTo(ActualResult));
        }

        [Test]
        public void ReplyTweet_ValidResponse()
        {
            var tweet = _fixture.Create<Tweet>();
            var tweetMessage = _fixture.Create<TweetMessage>();
            _mockTweetRepo.Setup(x => x.ReplyTweet(It.IsAny<string>(), It.IsAny<TweetMessage>())).Returns(tweet);
            var ActualResult = _tweetService.ReplyTweet("test", "11112233445566", tweetMessage);
            Assert.IsNotNull(ActualResult);
            Assert.That(tweet, Is.EqualTo(ActualResult));
        }

        [Test]
        public void ReplyTweet_ShouldThrow_BadRequestException()
        {
            var exception = Assert.Throws<DomainException>(() => _tweetService.ReplyTweet("test", "", null));
            Assert.AreEqual(exception.HttpStatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(exception.Message, "Id Cannot be empty");
        }

        [Test]
        public void DeleteTweet_ValidResponse()
        {
            _mockTweetRepo.Setup(x => x.RemoveTweet(It.IsAny<string>())).Returns(0);
            var ActualResult = _tweetService.DeleteTweet("11112233445566");
            Assert.IsNotNull(ActualResult);
            Assert.That(0, Is.EqualTo(ActualResult));
        }
    }
}
