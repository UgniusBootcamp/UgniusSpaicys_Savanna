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

        public const int LionSpeed = 3; // Lion speed

        public const int LionVision = 6; // Lion vision
        
        public const int AntelopeSpeed = 6; // Antelope speed
        
        public const int AntelopeVision = 3; // Antelope vision
        
        public const string Lion = "L"; // Lion name
        
        public const string Antelope = "A"; // Antelope name
        
        public const string UnknownAnimalType = "Unknown animal type to create"; // Unknown animal type message
        
        public const string Header = "Antelopes - {0} | Lions - {1}"; // Header message

        public const double LionHealth = 50; // Lion health

        public const double AntelopeHealth = 30; // Antelope health

        public const double HealthDamageOnMove = 0.5; // Health taken on every move 

        public const double HealthIncreaseOnAntelopeEaten = 15; // Health added when antelope is eaten
    }
}
