using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.UI
{
    public class MapPrinter : IMapPrinter
    {
        /// <summary>
        /// Method for printing map to console
        /// </summary>
        /// <param name="header">Header message</param>
        /// <param name="map">Map</param>
        public void PrintMap(string header, string footer, IMap map)
        {
            int yOffset = 0;
            int xOffset = 0;

            Console.CursorVisible = false;
            Console.SetCursorPosition(xOffset, yOffset);
            Console.Write(new string(' ', map.Width));
            Console.SetCursorPosition(xOffset, yOffset++);
            Console.Write(header);
            Console.SetCursorPosition(xOffset, yOffset);
            Console.Write(GameConstants.MapCorner + new string(GameConstants.MapHorizontalBorder, map.Width) + GameConstants.MapCorner);
            for (int y = 0; y < map.Height; y++)
            {
                Console.SetCursorPosition(xOffset, ++yOffset);
                Console.Write(GameConstants.MapVerticalBorder);
                for (int x = 0; x < map.Width; x++)
                {
                    var animal = map.GetAnimal(x, y);
                    Console.Write(animal != null ? animal.Name : GameConstants.FreeMapSpace); ;
                }
                Console.Write(GameConstants.MapVerticalBorder);
            }
            Console.SetCursorPosition(xOffset, ++yOffset);
            Console.Write(GameConstants.MapCorner + new string(GameConstants.MapHorizontalBorder, map.Width) + GameConstants.MapCorner);
            Console.SetCursorPosition(xOffset, ++yOffset);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(xOffset, yOffset++);
            Console.WriteLine(footer);
            Console.ResetColor();
        }
    }
}
