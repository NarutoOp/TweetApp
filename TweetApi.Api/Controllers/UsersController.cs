﻿namespace TweetApp.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TweetApp.Domain.Interfaces.User;
    using TweetApp.Domain.Models.Users;

    /// <summary>
    /// UsersController class
    /// </summary>
    [Route("api/v1.0/tweets")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// IUserService instance
        /// </summary>
        private readonly IUserService _registerUserService;

        /// <summary>
        /// UsersController constructor
        /// </summary>
        public UsersController(IUserService registerUserService)
        {
            _registerUserService = registerUserService;
        }

        /// <summary>
        /// User Registeration controller
        /// </summary>
        /// <param name="user">User instance</param>
        /// <returns></returns>
        [Route("register")]
        [HttpGet]
        public ActionResult Register()
        {
            throw new InternalBufferOverflowException();
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
            var result = _registerUserService.Create(user);
            return Ok(result);
        }
        
        /// <summary>
        /// Login Controller
        /// </summary>
        /// <param name="userDetails">Login instance</param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromBody] UserLogin userDetails)
        {
            return _registerUserService.Login(userDetails) ? Ok("Verified") : Ok("failed");
        }

        /// <summary>
        /// Forgot Controller for updating password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("{userName}/forgot")]
        [HttpPut]
        public ActionResult<User> Forgot([FromRoute] string userName,[FromBody] string password)
        {
            UserLogin userLogin = new UserLogin
            {
                UserName =  userName,
                Password = password
            };
            return _registerUserService.UpdatePassword(userLogin);
        }
    }
}
