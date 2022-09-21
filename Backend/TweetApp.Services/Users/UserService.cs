namespace TweetApp.Services.Users
{
    using Microsoft.Extensions.Configuration;
    using TweetApp.Domain.Exceptions;
    using TweetApp.Domain.Interfaces.User;
    using TweetApp.Domain.Models.Users;
    using TweetApp.Domain.Validators;
    using TweetApp.Services.Utility;

    /// <summary>
    /// RegisterUserService class
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// IUserRepository instance
        /// </summary>
        private readonly IUserRepo _userRepository;

        /// <summary>
        /// IConfiguration instance
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// UserService constructor
        /// </summary>
        /// <param name="userRepository">IUserRepository instance</param>
        public UserService(IUserRepo userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// GetAllUsers method
        /// </summary>
        /// <returns>List of users</returns>
        public List<UserResponse> GetAllUsers()
        {
            var responses = _userRepository.GetAllUser();
            List<UserResponse> userResponse = new List<UserResponse>();

            foreach (var response in responses)
            {
                userResponse.Add(
                    new UserResponse
                    {
                        Id = response.Id,
                        UserName = response.LoginId,
                        FirstName = response.FirstName,
                        LastName = response.LastName,
                        Email = response.Email,
                        ContactNumber = response.ContactNumber,
                    });
            }
            return userResponse;
        }

        /// <summary>
        /// GetUserByUsername method
        /// </summary>
        /// <returns>List of users</returns>
        public List<UserResponse> GetUserByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new DomainException("Username cannot be empty", System.Net.HttpStatusCode.BadRequest);
            }
            var responses = _userRepository.GetUserByUsername(username);
            List<UserResponse> userResponse = new List<UserResponse>();
            foreach(var response in responses)
            {
                userResponse.Add(
                    new UserResponse
                    {
                        Id = response.Id,
                        UserName = response.LoginId,
                        FirstName = response.FirstName,
                        LastName = response.LastName,
                        Email = response.Email,
                        ContactNumber = response.ContactNumber,
                    });
            }
            return userResponse;
        }

        /// <summary>
        /// Add user into the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserResponse RegisterUser(User user)
        {
            Validations.EnsureValid(user, new UserRequestValidator(user));
            user.Id = DateTime.Now.ToString("yyyyMMddHHmmss");
            string hash = PasswordHasher.ConvertToHash(user.Password);
            user.Password = hash;
            var response =  _userRepository.AddUser(user);
            return new UserResponse
            {
                Id = response.Id,
                UserName = response.LoginId,
                FirstName = response.FirstName,
                LastName = response.LastName,
                Email = response.Email,
                ContactNumber = response.ContactNumber,
            };
        }

        public LoginResponse Login(UserLogin userLogin)
        {
            Validations.EnsureValid(userLogin, new LoginValidator(userLogin));
            var getUser = _userRepository.GetUserByUsername(userLogin.UserName)?.FirstOrDefault();
            if (getUser == null)
            {
                throw new DomainException("User not found", System.Net.HttpStatusCode.BadRequest);
            }
            var (isVerified, needsUpgrade) = PasswordHasher.CheckPassword(getUser.Password, userLogin.Password);

            if (!isVerified)
            {
                throw new DomainException("Wrong Password", System.Net.HttpStatusCode.BadRequest);
            }

            var token = WebToken.GenerateJSONWebToken(userLogin, _configuration.GetSection("Jwt:Key").Value);

            var response = new LoginResponse
            {
                Name = $"{getUser.FirstName} {getUser.LastName}",
                UserName = getUser.LoginId,
                Token = token
            };

            return response;
        }

        public User UpdatePassword(UserLogin userLogin)
        {
            Validations.EnsureValid(userLogin, new LoginValidator(userLogin));
            var getUser = _userRepository.GetUserByUsername(userLogin.UserName)?.FirstOrDefault();
            if (getUser == null)
            {
                throw new DomainException("User not found", System.Net.HttpStatusCode.BadRequest);
            }
            string hash = PasswordHasher.ConvertToHash(userLogin.Password);
            getUser.Password = hash;

            return _userRepository.UpdateUser(getUser);
        }
    }
}
