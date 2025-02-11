using SavannaApp.Data.Constants;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.UI
{
    public class MapPrinter : IMapPrinter
    {
        public void PrintMap(string header, IMap map)
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
            for (int i = 0; i < map.Height; i++)
            {
                Console.SetCursorPosition(xOffset, ++yOffset);
                Console.Write(GameConstants.MapVerticalBorder);
                for (int j = 0; j < map.Width; j++)
                {
                    var animal = map.GetAnimal(i, j);
                    Console.Write(animal != null ? animal.Name : "·"); ;
                }
                Console.Write(GameConstants.MapVerticalBorder);
            }
            Console.SetCursorPosition(xOffset, ++yOffset);
            Console.Write(GameConstants.MapCorner + new string(GameConstants.MapHorizontalBorder, map.Width) + GameConstants.MapCorner);

        }
    }
}
