namespace TweetApp.Domain
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// FieldValidator class
    /// </summary>
    public class FieldValidator
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
    }
}
