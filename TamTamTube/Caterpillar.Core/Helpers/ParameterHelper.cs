using Caterpillar.Core.Exception;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Caterpillar.Core.Helpers
{
    /// <summary>
    /// Contains helper methods for parameter validations
    /// </summary>
    public static class ParameterHelper
    {
        /// <summary>
        /// Checks for string parameter and coerces its value.
        /// </summary>
        /// <param name="param">Parameter to chech and trim</param>
        /// <param name="checkForNull"></param>
        /// <param name="checkIfEmpty"></param>
        /// <param name="checkForCommas"></param>
        /// <param name="maxSize"></param>
        /// <param name="paramName"></param>
        public static void CheckForNull(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCharacter, string characterToCheck, int maxSize, string paramName) 
        {
            if (param == null)
            {
                if (checkForNull)
                {
                    throw new ArgumentNullException(paramName);
                }

                return;
            }

            param = param.Trim();
            if (checkIfEmpty && param.Length < 1)
            {
                throw new ArgumentNullOrEmptyException(string.Format(ExceptionMessages.ParameterCannotBeEmpty, paramName));
            }

            if (maxSize > 0 && param.Length > maxSize)
            {
                throw new ArgumentLengthExceedsException(string.Format(ExceptionMessages.ParameterCannotBeEmpty, paramName));
            }

            if (checkForCharacter && param.Contains(characterToCheck))
            {
                throw new ArgumentContainsInvalidCharacterException(string.Format(ExceptionMessages.InvalidCharacterInParameter, checkForCharacter, paramName));    
            }
        }

        public static void CheckForNull(string param, string paramName) 
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void CheckForEmpty(string param, string paramName)
        {
            string trimmedValue = param.Trim();

            if (trimmedValue == string.Empty || trimmedValue.Length == 0)
            {
                throw new ArgumentNullOrEmptyException(string.Format(ExceptionMessages.ParameterCannotBeEmpty, paramName));
            }
        }

        public static void CheckForMinimumLength(string param, int minLength, string paramName) 
        {
            if (param.Length < minLength)
            {
                throw new ArgumentLengthShortException(string.Format(ExceptionMessages.ParameterLengthIsTooShort, paramName));
            }
        }

        public static void CheckForMaximumLength(string param, int maxLength, string paramName)
        {
            if (param.Length > maxLength)
            {
                throw new ArgumentLengthExceedsException(string.Format(ExceptionMessages.ParameterLengthExceeds, paramName));
            }
        }

        public static void CheckForInvalidCharacter(string param, string characterToCheck, string paramName)
        {
            if (param.Contains(characterToCheck))
            {
                throw new ArgumentContainsInvalidCharacterException(string.Format(ExceptionMessages.InvalidCharacterInParameter, characterToCheck, paramName));
            }
        }

        public static void CheckForInvalidCharacters(string param, string[] charactersToCheck, string paramName)
        {
            for (int i = 0; i < charactersToCheck.Length; i++)
            {
                if (param.Contains(charactersToCheck[i]))
                {
                    throw new ArgumentContainsInvalidCharacterException(string.Format(ExceptionMessages.InvalidCharacterInParameter, charactersToCheck[i], paramName));
                }    
            }
        }

        public static void CheckForAlphanumericCharacterCount(string param, bool forMinimumCount, int alpanumericCharacterCount, string paramName) 
        {
            //http://stackoverflow.com/questions/3210393/how-do-i-remove-all-non-alphanumeric-characters-from-a-string-except-dash

            string result = null;
            /* Regex Version*/
            //Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            //result = rgx.Replace(param, "");

            /*Non-Regex Version and it is faster than regex*/
            char[] arr = param.ToCharArray();
            arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c)
                                              || char.IsWhiteSpace(c)
                                              || c == '-')));
            result = new string(arr);

            if (forMinimumCount)    // Check for minimum count
            {
                if (result != null || result != string.Empty)
                {
                    if (result.Length < alpanumericCharacterCount)
                    {
                        //throw ex that shows param doesnt contain enough alphanumeric characther
                    }
                }
                else
                {
                    if (alpanumericCharacterCount >= 0)
                    {
                        //throw ex that shows param doesnt contain enough alphanumeric characther
                    }
                }
            }
            else                    // Check for maximum count
            {
                if (result != null)
                {
                    if (result.Length > alpanumericCharacterCount)
                    {
                        //throw ex that shows param contains too much alphanumeric character
                    }
                }
            }
        }

        public static void CheckForNonAlphanumericCharacter(string param, bool forMinimumCount, int nonAlpanumericCharacterCount, string paramName)
        {
            //http://stackoverflow.com/questions/3114027/regex-expressions-for-all-non-alphanumeric-symbols

            string result = null;
            Regex pattern = new Regex(@"\W|_");
            Match match = pattern.Match(param);
            
            result = match.Value;
            if (forMinimumCount)    // Check for minimum count
            {
                if (result != null || result != string.Empty)
                {
                    if (result.Length < nonAlpanumericCharacterCount)
                    {
                        //throw ex that shows param doesnt contain enough nonalphanumeric characther
                    }
                }
                else
                {
                    if (nonAlpanumericCharacterCount >= 0)
                    {
                        //throw ex that shows param doesnt contain enough nonalphanumeric  characther
                    }
                }
            }
            else                    // Check for maximum count
            {
                if (result != null)
                {
                    if (result.Length > nonAlpanumericCharacterCount)
                    {
                        //throw ex that shows param contains too much nonalphanumeric  character
                    }
                }
            }
        }

        public static bool ValidateForLength(string parameter, int length, bool forMaxLength) {
            if (forMaxLength)
            {
                return parameter.Length <= length;
            }
            else
            {
                return parameter.Length >= length;
            }
        }

        public static bool ValidateForNull(object value)
        {
            return value != null;
        }

        public static bool CheckGuidNotEmpty(Guid guid, string paramName) 
        {
            if (guid == Guid.Empty)
            {
                 throw new ArgumentNullOrEmptyException(string.Format(ExceptionMessages.ParameterCannotBeEmpty, paramName));
            }

            return true;
        }

        public static void CheckForNullForObject(object obj, string parameterName) 
        {
            if (obj == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void CheckForArrayLength(Array array, int lengthCount, string parameterName)
        {
            if (array.Length != lengthCount)
            {
                throw new ArgumentException(parameterName);
            }
        }

        public static void CheckForDateTimeRange(DateTime startDate, DateTime endDate, string parameterName, string message) 
        {
            if (startDate.Ticks > endDate.Ticks)
            {
                throw new ArgumentOutOfRangeException(parameterName, message);
            }
        }

        public static void IsValidEmail(string email, string paramName) 
        {
            try
            {
                MailAddress mail = new MailAddress(email);
            }
            catch (System.Exception)
            {
                throw new ArgumentLengthShortException(string.Format(ExceptionMessages.NotValidMailAddress, paramName));   
            }
        }
    }
}
