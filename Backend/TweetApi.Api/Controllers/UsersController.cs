namespace TweetApp.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TweetApp.Domain.Exceptions;
    using TweetApp.Domain.Interfaces.User;
    using TweetApp.Domain.Models.Users;

    /// <summary>
    /// UsersController class
    /// </summary>
    [Route("api/v1.0/tweets")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        /// <summary>
        /// IUserService instance
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// UsersController constructor
        /// </summary>
        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// GetAllUser controller
        /// </summary>
        /// <param name="user">Users List</param>
        /// <returns></returns>
        [Route("users/all")]
        [HttpGet]
        public ActionResult GetAllUser()
        {
            var result = _userService.GetAllUsers();
            _logger.LogInformation("GetAllUser - {status} {httpStatusCode}", "success", "200");
            return Ok(result);
        }

        /// <summary>
        /// GetAllUser controller
        /// </summary>
        /// <param name="user">Users List</param>
        /// <returns></returns>
        [Route("user/search/{username}")]
        [HttpGet]
        public ActionResult SearchUser(string username)
        {
            var result = _userService.GetUserByUsername(username);
            _logger.LogInformation("SearchUser - {status} {httpStatusCode}", "success", "200");
            return Ok(result);
        }

        /// <summary>
        /// User Registeration controller
        /// </summary>
        /// <param name="user">User instance</param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public ActionResult Register([FromBody] User user)
        {
            if(user == null)
            {
                throw new DomainException("Invalid Request", System.Net.HttpStatusCode.BadRequest);
            }
            var result = _userService.RegisterUser(user);
            _logger.LogInformation("Register - {status} {httpStatusCode}", "success", "200");
            return Ok(result);
        }
        
        /// <summary>
        /// Login Controller
        /// </summary>
        /// <param name="userDetails">Login instance</param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromBody] UserLogin userLogin)
        {
            if (userLogin == null)
            {
                throw new DomainException("Invalid Request", System.Net.HttpStatusCode.BadRequest);
            }
            var result = _userService.Login(userLogin);
            _logger.LogInformation("Login - {status} {httpStatusCode}", "success", "200");
            return Ok(result);
        }

        /// <summary>
        /// Forgot Controller for updating password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("{userName}/forgot")]
        [HttpPut]
        public ActionResult Forgot([FromRoute] string userName,[FromBody] string password)
        {
            UserLogin userLogin = new UserLogin
            {
                UserName =  userName,
                Password = password
            };
            _logger.LogInformation("Forgot - {status} {httpStatusCode}", "success", "200");
            return Ok(_userService.UpdatePassword(userLogin));
        }
    }
}
