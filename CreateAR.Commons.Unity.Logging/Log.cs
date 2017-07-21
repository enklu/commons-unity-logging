namespace CreateAR.Commons.Unity.Logging
{
    /// <summary>
    /// Terse logging interface for a Unity client.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Defines a delegate for receiving an event.
        /// </summary>
        /// <param name="level">The level of the log.</param>
        /// <param name="caller">The object that sent the log.</param>
        /// <param name="message">The message to send.</param>
        public delegate void LogEvent(LogLevel level, object caller, string message);

        /// <summary>
        /// Called when log has been sent.
        /// </summary>
        public static event LogEvent OnLog;

        /// <summary>
        /// Filters all logs below value. Eg:
        /// 
        /// Filter = LogLevel.Warning;
        /// 
        /// This will filter out Info and Debug level logs.
        /// </summary>
        public static LogLevel Filter = LogLevel.Debug;

        /// <summary>
        /// Logs a message at a specific level.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="caller"></param>
        /// <param name="message"></param>
        /// <param name="replacements"></param>
        public static void Out(
            LogLevel level,
            object caller,
            object message,
            params object[] replacements)
        {
            if (level < Filter)
            {
                return;
            }

            if (replacements.Length > 0)
            {
                message = string.Format(message.ToString(), replacements);
            }

            OnLog?.Invoke(level, caller, message.ToString());
        }
        
        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <param name="caller">The calling object or null.</param>
        /// <param name="message">The object to log.</param>
        /// <param name="replacements">String replacements.</param>
        public static void Debug(object caller, object message, params object[] replacements)
        {
            Out(
                LogLevel.Debug,
                caller,
                message,
                replacements);
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <param name="caller">The calling object or null.</param>
        /// <param name="message">The object to log.</param>
        /// <param name="replacements">String replacements.</param>
        public static void Info(object caller, object message, params object[] replacements)
        {
            Out(
                LogLevel.Info,
                caller,
                message,
                replacements);
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <param name="caller">The calling object or null.</param>
        /// <param name="message">The object to log.</param>
        /// <param name="replacements">String replacements.</param>
        public static void Warning(object caller, object message, params object[] replacements)
        {
            Out(
                LogLevel.Warning,
                caller,
                message,
                replacements);
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <param name="caller">The calling object or null.</param>
        /// <param name="message">The object to log.</param>
        /// <param name="replacements">String replacements.</param>
        public static void Error(object caller, object message, params object[] replacements)
        {
            Out(
                LogLevel.Error,
                caller,
                message,
                replacements);
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <param name="caller">The calling object or null.</param>
        /// <param name="message">The object to log.</param>
        /// <param name="replacements">String replacements.</param>
        public static void Fatal(object caller, object message, params object[] replacements)
        {
            Out(
                LogLevel.Fatal,
                caller,
                message,
                replacements);
        }
    }
}