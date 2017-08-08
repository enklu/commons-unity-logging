namespace CreateAR.Commons.Unity.Logging
{
    /// <summary>
    /// Interface for log targets.
    /// </summary>
    public interface ILogTarget
    {
        /// <summary>
        /// When called, logs to the file.
        /// </summary>
        /// <param name="level">The log's level.</param>
        /// <param name="caller">The calling object.</param>
        /// <param name="message">The message to log.</param>
        void OnLog(LogLevel level, object caller, string message);
    }
}