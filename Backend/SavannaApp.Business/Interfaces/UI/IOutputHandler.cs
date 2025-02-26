namespace SavannaApp.Business.Interfaces.UI
{
    public interface IOutputHandler
    {
        /// <summary>
        /// display message
        /// </summary>
        /// <param name="message">message to display</param>
        public void Output(string message);

        /// <summary>
        /// clear output
        /// </summary>
        public void Clear();
    }
}