﻿using SavannaApp.Business.Interfaces.Web;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Helpers.Exceptions;

namespace SavannaApp.Business.Services.Web
{
    public class ValidationService : IValidationService
    {
        public void ValidateRegisterPassword(string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                throw new BusinessRuleValidationException(WebServiceConstants.PasswordsDoNotMatch);
            }
        }
    }
}
