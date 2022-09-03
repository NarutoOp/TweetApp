namespace TweetApi.Tests.Controller
{
    using AutoFixture;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;
    using System.Net;
    using TweetApp.Api.Controllers;
    using TweetApp.Domain.Exceptions;
    using TweetApp.Domain.Interfaces.User;
    using TweetApp.Domain.Models.Users;

    public class UsersControllerTest
    {
        private Mock<IUserService> _mockUserService;
        private IFixture _fixture;
        private UsersController _userController;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockUserService = new Mock<IUserService>();
            _userController = new UsersController(_mockUserService.Object);
        }

        [Test]
        public void GetAllUser_ValidResponse()
        {
            var UserList = _fixture
                            .CreateMany<User>(2)
                            .ToList();
            _mockUserService.Setup(x => x.GetAllUsers()).Returns(UserList);
            var ActualResult = _userController.GetAllUser();
            Assert.IsNotNull(ActualResult);
            Assert.IsInstanceOf<OkObjectResult>(ActualResult);
        }

        [Test]
        public void SearchUser_ValidResponse()
        {
            var UserList = _fixture
                            .CreateMany<User>(2)
                            .ToList();
            _mockUserService.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(UserList);
            var ActualResult = _userController.SearchUser(It.IsAny<string>());
            Assert.IsNotNull(ActualResult);
            Assert.IsInstanceOf<OkObjectResult>(ActualResult);
        }

        [Test]
        public void Register_ValidResponse()
        {
            var user = _fixture
                            .Create<User>();
            _mockUserService.Setup(x => x.RegisterUser(It.IsAny<User>())).Returns(user);
            var ActualResult = _userController.Register(user);
            Assert.IsNotNull(ActualResult);
            Assert.IsInstanceOf<OkObjectResult>(ActualResult);
        }

        [Test]
        public void Register_ShouldThrow_BadRequestException()
        {
            var exception = Assert.Throws<DomainException>(() => _userController.Register(null));
            Assert.AreEqual(exception.HttpStatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(exception.Message, "Invalid Request");
        }

        [Test]
        public void Login_ValidResponse()
        {
            var userLogin = _fixture.Create<UserLogin>();
            var userLoginResponse = _fixture.Create<LoginResponse>();
            _mockUserService.Setup(x => x.Login(It.IsAny<UserLogin>())).Returns(userLoginResponse);
            var ActualResult = _userController.Login(userLogin);
            Assert.IsNotNull(ActualResult);
            Assert.IsInstanceOf<OkObjectResult>(ActualResult);
        }

        [Test]
        public void Login_ShouldThrow_BadRequestException()
        {
            var exception = Assert.Throws<DomainException>(() => _userController.Login(null));
            Assert.AreEqual(exception.HttpStatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(exception.Message, "Invalid Request");
        }

        [Test]
        public void Forgot_ValidResponse()
        {
            var user = _fixture.Create<User>();
            _mockUserService.Setup(x => x.UpdatePassword(It.IsAny<UserLogin>())).Returns(user);
            var ActualResult = _userController.Forgot("","");
            Assert.IsNotNull(ActualResult);
            Assert.IsInstanceOf<OkObjectResult>(ActualResult);
        }
    }
}
