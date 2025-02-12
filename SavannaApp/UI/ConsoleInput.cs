using GameOfLife.Data.Interfaces.UI;

namespace GameOfLife.UI
{
    public class ConsoleInput : IInputHandler
    {
        /// <summary>
        /// Method to get integer from user input
        /// </summary>
        /// <param name="message">message for user</param>
        /// <returns>user input as integer</returns>
        public int GetInt(string message)
        {
            Console.WriteLine(message);
            int.TryParse(Console.ReadLine(), out int result);

            return result;
        }

        /// <summary>
        /// Method to get string from user input
        /// </summary>
        /// <param name="message">message for user</param>
        /// <returns>user input as string</returns>
        public string? GetString(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }
}