using SavannaApp.Business.Interfaces.Web;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Helpers.Exceptions;

namespace SavannaApp.Business.Services.Web
{
    public class ValidationService : IValidationService
    {
        /// <summary>
        /// Method to check if password is valid
        /// </summary>
        /// <param name="password">password</param>
        /// <param name="confirmPassword">confirmed password</param>
        public void ValidateRegisterPassword(string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                throw new BusinessRuleValidationException(WebServiceConstants.PasswordsDoNotMatch);
            }
        }
    }
}
