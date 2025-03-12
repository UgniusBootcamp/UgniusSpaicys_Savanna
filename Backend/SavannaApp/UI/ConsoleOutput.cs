using SavannaApp.Business.Interfaces.UI;

namespace SavannaApp.UI
{
    public class ConsoleOutput : IOutputHandler
    {
        /// <summary>
        /// Method for user output to console
        /// </summary>
        /// <param name="message">message to user</param>
        public void Output(string message)
        {
            Console.WriteLine(message);
        }


        /// <summary>
        /// Method to clear console window
        /// </summary>
        public void Clear() { Console.Clear(); }
    }
}