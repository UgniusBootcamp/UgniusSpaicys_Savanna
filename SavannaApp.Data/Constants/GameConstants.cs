using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public const int DefaultMapLength = 50;
    }
}
