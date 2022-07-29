namespace TweetApp.DAL.Repository
{
    using MongoDB.Driver;
    using TweetApp.DAL.Models.Users;
    using TweetApp.Domain.Models.Users;
    using TweetApp.Domain.Exceptions;
    using TweetApp.DAL.Translator;
    using TweetApp.Domain.Interfaces.User;
    using System.Net;

    /// <summary>
    /// UserRepository class
    /// </summary>
    public class UserRepo: Configuration, IUserRepo
    {
        /// <summary>
        /// MongoCollection instance
        /// </summary>
        private readonly IMongoCollection<UserDTO> _userCollection;

        /// <summary>
        /// UserRepository constructor
        /// </summary>
        /// <param name="mongoClient">IMongoClient instance</param>
        public UserRepo(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(_databaseName);
            _userCollection = database.GetCollection<UserDTO>(_userCollectionName);
        }

        // <summary>
        /// GetUser from database
        /// </summary>
        /// <returns>User instance</returns>
        public List<User> GetAllUser()
        {
            var listOfUserDTO = _userCollection.Find(user => true).ToList();
            var listOfUser = listOfUserDTO.ConvertAll(x => UserTranslator.UserDtoToUser(x)).ToList();
            return listOfUser;

        }

        /// <summary>
        /// GetUser by Id from database
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="isEmailId">To check whether id passed is email or username</param>
        /// <returns>User instance</returns>
        public User GetUser(string id)
        {
            var userDTO = _userCollection.Find(user => user.LoginId == id).FirstOrDefault();

            return UserTranslator.UserDtoToUser(userDTO);
        }

        /// <summary>
        /// Add user into the database
        /// </summary>
        /// <param name="user">User instance</param>
        /// <returns>User instance</returns>
        public User AddUser(User user)
        {
            if (_userCollection.Find(x => x.Email == user.Email).Any())
            {
                throw new DomainException("Email Id is already exist.", HttpStatusCode.BadRequest);
            }
            if (_userCollection.Find(x => x.LoginId == user.LoginId).Any())
            {
                throw new DomainException("LoginId (username) is already exist.", HttpStatusCode.BadRequest);
            }

            var userDTO = UserTranslator.UserToUserDTO(user);
            _userCollection.InsertOne(userDTO);
            return user;
        }

        public void RemoveUser(string id)
        {
            _userCollection.DeleteOne(user => user.Id == id);
        }

        public User UpdateUser(User user)
        {
            var userDTO = UserTranslator.UserToUserDTO(user);
            _userCollection.ReplaceOne(x => x.LoginId == userDTO.LoginId, userDTO);
            return user;
        }
    }
}
