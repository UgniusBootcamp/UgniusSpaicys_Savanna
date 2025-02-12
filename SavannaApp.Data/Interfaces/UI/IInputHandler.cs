namespace GameOfLife.Data.Interfaces.UI
{
    public interface IInputHandler
    {
        /// <summary>
        /// Method to get integer from user input
        /// </summary>
        /// <param name="message">message for user</param>
        /// <returns>user input as integer</returns>
        int GetInt(string message);

        /// <summary>
        /// Method to get string from user input
        /// </summary>
        /// <param name="message">message for user</param>
        /// <returns>user input as string</returns>
        string? GetString(string message);
    }
}