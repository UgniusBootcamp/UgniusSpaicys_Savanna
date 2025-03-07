namespace SavannaApp.Data.Enums
{
    public class UserRoles
    {
        public const string User = nameof(User);

        /// <summary>
        /// Method to get all roles
        /// </summary>
        public static readonly IReadOnlyCollection<string> AllRoles = new List<string>
        {
            User
        };
    }
}
