namespace SavannaApp.Business.Interfaces.Web
{
    public interface IValidationService
    {
        void ValidateRegisterPassword(string password, string confirmPassword);
    }
}
