namespace SavannaApp.Data.Enums
{
    public class UserRoles
    {
        public const string User = nameof(User);

        public static readonly IReadOnlyCollection<string> AllRoles = new List<string>
        {
            User
        };
    }
}
