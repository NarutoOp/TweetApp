namespace TweetApp.Services.Users
{
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
        /// UserService constructor
        /// </summary>
        /// <param name="userRepository">IUserRepository instance</param>
        public UserService(IUserRepo userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Add user into the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User RegisterUser(User user)
        {
            Validations.EnsureValid(user, new UserRequestValidator(user));
            user.Id = DateTime.Now.ToString("yyyyMMddHHmmss");
            string hash = PasswordHasher.Hash(user.Password);
            user.Password = hash;
            _userRepository.AddUser(user);
            return user;
        }

        public bool Login(UserLogin userLogin)
        {
            Validations.EnsureValid(userLogin, new LoginValidator(userLogin));
            var getUser = _userRepository.GetUser(userLogin.UserName);
            var (isVerified, needsUpgrade) = PasswordHasher.Check(getUser.Password, userLogin.Password);

            return isVerified;
        }

        public User UpdatePassword(UserLogin userLogin)
        {
            Validations.EnsureValid(userLogin, new LoginValidator(userLogin));

            var getUser = _userRepository.GetUser(userLogin.UserName);
            string hash = PasswordHasher.Hash(userLogin.Password);
            getUser.Password = hash;

            return _userRepository.UpdateUser(getUser);
        }
    }
}
