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
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// UserService constructor
        /// </summary>
        /// <param name="userRepository">IUserRepository instance</param>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Add user into the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User Create(User user)
        {
            Validations.EnsureValid(user, new UserRequestValidator(user));
            string hash = PasswordHasher.Hash(user.Password);
            user.Password = hash;
            _userRepository.Create(user);
            return user;
        }

        public bool Login(UserLogin userLogin)
        {
            Validations.EnsureValid(userLogin, new LoginValidator(userLogin));
            var getUser = _userRepository.Get(userLogin.UserName, false);
            var (isVerified, needsUpgrade) = PasswordHasher.Check(getUser.Password, userLogin.Password);

            return isVerified;
        }

        public User UpdatePassword(UserLogin userLogin)
        {
            Validations.EnsureValid(userLogin, new LoginValidator(userLogin));

            var getUser = _userRepository.Get(userLogin.UserName, false);
            string hash = PasswordHasher.Hash(userLogin.Password);
            getUser.Password = hash;

            return _userRepository.Update(getUser);
        }
    }
}
