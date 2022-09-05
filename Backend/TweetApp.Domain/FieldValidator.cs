namespace TweetApp.Domain
{
    using System.Net;
    using System.Text.RegularExpressions;
    using TweetApp.Domain.Exceptions;

    /// <summary>
    /// FieldValidator class
    /// </summary>
    public static class FieldValidator
    {
        /// <summary>
        /// Validates Alphabetical value
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool isValidAlphabet(string inputData)
        {
            if(inputData != null)
            {
                var match = Regex.Match(inputData, "^[A-Za-z]+$", RegexOptions.Compiled);
                return match.Success;
            }
            return true;
        }

        /// <summary>
        /// Validates AlphaNumeric value
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool isValidAlphaNumeric(string inputData)
        {
            if (inputData != null)
            {
                var match = Regex.Match(inputData, "^[A-Za-z0-9]+$", RegexOptions.Compiled);
                return match.Success;
            }
            return true;
        }

        /// <summary>
        /// Validates numeric value
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool isValidNumeric(string inputData)
        {
            if (inputData != null)
            {
                var match = Regex.Match(inputData, "^[0-9]+$", RegexOptions.Compiled);
                return match.Success;
            }
            return true;
        }

        /// <summary>
        /// Validates Id
        /// </summary>
        /// <param name="inputId"></param>
        /// <returns></returns>
        public static void IsValidId(string inputId)
        {
            if (string.IsNullOrEmpty(inputId))
            {
                throw new DomainException("Id Cannot be empty", HttpStatusCode.BadRequest);
            }
            if(inputId.Length != 14)
            {
                throw new DomainException("Invalid Id length", HttpStatusCode.BadRequest);
            }
            if (!isValidNumeric(inputId))
            {
                throw new DomainException("Id should be numeric", HttpStatusCode.BadRequest);
            }
        }
    }
}
