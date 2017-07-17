namespace CreateAR.Commons.Unity.Logging
{
    /// <summary>
    /// An interface for formatting logs.
    /// </summary>
    public interface ILogFormatter
    {
        /// <summary>
        /// Formats a log.
        /// </summary>
        /// <param name="level">The level of the log to be formatted.</param>
        /// <param name="caller">The calling object.</param>
        /// <param name="message">The message to log.</param>
        /// <returns></returns>
        string Format(LogLevel level, object caller, string message);
    }
}