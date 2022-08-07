namespace TweetApp.DAL.Translator
{
    using TweetApp.DAL.Models.Users;
    using TweetApp.Domain.Models.Users;

    /// <summary>
    /// UserTranslator class
    /// </summary>
    public static class UserTranslator
    {
        public static UserDTO UserToUserDTO(User user)
        {
            if(user == null)
            {
                return null;
            }

            var userDTO = new UserDTO
            {
                FirstName = user.FirstName,
                LastName =  user.LastName,
                Email = user.Email,
                LoginId = user.LoginId,
                Id = user.Id,
                ContactNumber = user.ContactNumber,
                Password = user.Password
            };

            return userDTO;
        }

        /// <summary>
        /// UserDTO to User Translator
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public static User UserDtoToUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return null;
            }

            var user = new User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                LoginId = userDTO.LoginId,
                Id = userDTO.Id,
                ContactNumber = userDTO.ContactNumber,
                Password = userDTO.Password
            };

            return user;
        }
    }
}
