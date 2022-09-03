namespace TweetApi.Tests.Services
{
    using AutoFixture;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using NUnit.Framework;
    using System.Net;
    using TweetApp.Domain.Exceptions;
    using TweetApp.Domain.Interfaces.User;
    using TweetApp.Domain.Models.Users;
    using TweetApp.Services.Users;

    public class UserServiceTest
    {
        private Mock<IUserRepo> _mockUserRepo;
        private Mock<IConfiguration> _mockConfiguration;
        private IFixture _fixture;
        private IUserService _userService;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockUserRepo = new Mock<IUserRepo>();
            _mockConfiguration = new Mock<IConfiguration>();
            _userService = new UserService(_mockUserRepo.Object, _mockConfiguration.Object);
        }

        [Test]
        public void GetAllUsers_ValidResponse()
        {
            var UserList = _fixture
                            .CreateMany<User>(2)
                            .ToList();
            _mockUserRepo.Setup(x => x.GetAllUser()).Returns(UserList);
            var ActualResult = _userService.GetAllUsers();
            Assert.IsNotNull(ActualResult);
            Assert.That(UserList, Is.EqualTo(ActualResult));
        }

        [Test]
        public void GetUserByUserName_Success()
        {
            var UserList = _fixture
                            .CreateMany<User>(2)
                            .ToList();
            _mockUserRepo.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(UserList);
            var ActualResult = _userService.GetUserByUsername("test");
            Assert.IsNotNull(ActualResult);
            Assert.That(UserList, Is.EqualTo(ActualResult));

        }

        [Test]
        public void GetUserByUserName_ShouldThrow_BadRequestException()
        {
            var exception = Assert.Throws<DomainException>(() => _userService.GetUserByUsername(""));
            Assert.AreEqual(exception.HttpStatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(exception.Message, "Username cannot be empty");
        }

        [Test]
        public void RegisterUser_ValidResponse()
        {
            var user = new User
            {
                Id = "1",
                FirstName = "testfirstname",
                LastName = "testlastname",
                Email = "test@test.com",
                LoginId = "testuser",
                ContactNumber = "1122334455",
                Password = "testpass",
                ConfirmPassword = "testpass"

            };
            _mockUserRepo.Setup(x => x.AddUser(It.IsAny<User>())).Returns(user);
            var ActualResult = _userService.RegisterUser(user);
            Assert.IsNotNull(ActualResult);
            Assert.That(ActualResult, Is.EqualTo(user));
        }

        [Test]
        public void RegisterUser_ShouldThrow_BadRequestException()
        {
            var userPasswordNotMatch = new User
            {
                Id = "1",
                FirstName = "testfirstname",
                LastName = "testlastname",
                Email = "test@test.com",
                LoginId = "testuser",
                ContactNumber = "1122334455",
                Password = "testpass",
                ConfirmPassword = "test"

            };

            var exception = Assert.Throws<DomainException>(() => _userService.RegisterUser(userPasswordNotMatch));
            Assert.AreEqual(exception.HttpStatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(exception.Errors.FirstOrDefault().Message, "Passwords should match");
        }

        [Test]
        public void Login_ValidResponse()
        {
            var user = new User
            {
                Id = "1",
                FirstName = "testfirstname",
                LastName = "testlastname",
                Email = "test@test.com",
                LoginId = "testuser",
                ContactNumber = "1122334455",
                Password = "10000.UWyiXwt5ArAi3WqHpm7aOQ==.we1lbEPbbntYnLkNNswdvX9DhrNNPR+1DhSviJdrGTM="
            };

            var userLogin = new UserLogin
            {
                UserName = "testuser",
                Password = "test12",
            };

            _mockUserRepo.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(new List<User> { user });
            _mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>()).Value).Returns("testkey for test");
            var ActualResult = _userService.Login(userLogin) ;
            Assert.IsNotNull(ActualResult);
            Assert.IsInstanceOf<LoginResponse>(ActualResult);
        }

        [Test]
        public void Login_ShouldThrow_BadRequestException()
        {
            var userLogin = new UserLogin
            {
                UserName = "testuser",
                Password = "testpass",
            };
            var exception = Assert.Throws<DomainException>(() => _userService.Login(userLogin));
            Assert.AreEqual(exception.HttpStatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(exception.Message, "User not found");
        }

        [Test]
        public void UpdatePassword_ValidResponse()
        {
            var user = new User
            {
                Id = "1",
                FirstName = "testfirstname",
                LastName = "testlastname",
                Email = "test@test.com",
                LoginId = "testuser",
                ContactNumber = "1122334455",
                Password = "10000.4IU4k7hLvMdN7SpmEU32Vw==.yfuawL2+X8cDo3SggsQYvUeIMMTVaHmrD4qMbcuy6rY="
            };

            var userLogin = new UserLogin
            {
                UserName = "testuser",
                Password = "testpass",
            };

            _mockUserRepo.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(new List<User> { user });
            _mockUserRepo.Setup(x => x.UpdateUser(It.IsAny<User>())).Returns(user);
            var ActualResult = _userService.UpdatePassword(userLogin);
            Assert.IsNotNull(ActualResult);
            Assert.That(user,Is.EqualTo(ActualResult));
        }

        [Test]
        public void UpdatePassword_ShouldThrow_BadRequestException()
        {
            var userLogin = new UserLogin
            {
                UserName = "testuser",
                Password = "testpass",
            };
            var exception = Assert.Throws<DomainException>(() => _userService.UpdatePassword(userLogin));
            Assert.AreEqual(exception.HttpStatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(exception.Message, "User not found");
        }
    }

}
