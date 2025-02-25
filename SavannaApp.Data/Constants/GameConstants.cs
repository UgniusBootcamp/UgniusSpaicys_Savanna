namespace SavannaApp.Data.Constants
{
    public static class GameConstants
    {
        public const char MapCorner = '+'; // Map corner representation

        public const char MapHorizontalBorder = '-'; // Map horizontal border representation

        public const char MapVerticalBorder = '|'; // Map vertical border representation

        public const string LenghtInputMessage = "Input map length 1 - "; // map length input message

        public const string HeightInputMessage = "Input map height 1 - "; // map height input message
    
        public const int DefaultMapHeight = 25; // default map size

        public const int DefaultMapLength = 50; // default map length

        public const string FreeMapSpace = "·"; // Free space on a map string

        public const string InvalidPosition = "Invalid Map position"; // Invalid position message

        public const string UnknownAnimalType = "Unknown animal type to create"; // Unknown animal type message
        
        public const double HealthDamageOnMove = 0.5; // Health taken on every move 

        public const double HealthIncreaseOnPrayEaten = 15; // Health added when antelope is eaten
        
        public const int DefaultRebirthRadius = 1; // rebirth radius

        public const int DefaultRebirthCounter = 3; // number of round of animals being together to make rebirth

        public const string PluginsDirectory = "Plugins"; // Plugins directory

        public const string DllFileExtensions = ".dll"; // dll file extension

        public const string DllLoadingFailed = "System has failed loading assembly"; // assembly loading fail message

        public const string MappingFails = "Failed mapping types into creatable objects"; // mapping fail message

        public const string FileNotFound = "File {0} has not been found";

        public const string ConfigLoadFail = "Config has failed to load {0}";

        public const string NoConstructor = "Animal does not have required constructor"; // no required constructor
        public const string ConfigDirectory = "Config";
        public const string AnimalConfiguration = "config.json";
    }
}
