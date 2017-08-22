using System.Collections.Generic;

namespace CreateAR.Commons.Unity.Logging
{
    /// <summary>
    /// Terse logging interface for a Unity client.
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// List of targets.
        /// </summary>
        private static readonly List<ILogTarget> _targets = new List<ILogTarget>();

        /// <summary>
        /// Defines a delegate for receiving an event.
        /// </summary>
        /// <param name="level">The level of the log.</param>
        /// <param name="caller">The object that sent the log.</param>
        /// <param name="message">The message to send.</param>
        public delegate void LogEvent(LogLevel level, object caller, string message);

        /// <summary>
        /// All log targets.
        /// </summary>
        public static ILogTarget[] Targets => _targets.ToArray();

        /// <summary>
        /// Adds an ILogTarget implementation.
        /// </summary>
        /// <param name="target">The target to add.</param>
        public static void AddLogTarget(ILogTarget target)
        {
            if (!_targets.Contains(target))
            {
                _targets.Add(target);
            }
        }

        /// <summary>
        /// Removes an ILogTarget implementation.
        /// </summary>
        /// <param name="target">The target to add.</param>
        public static void RemoveLogTarget(ILogTarget target)
        {
            _targets.Remove(target);
        }
        
        /// <summary>
        /// Filters all logs below value. Eg:
        /// 
        /// Filter = LogLevel.Warning;
        /// 
        /// This will filter out Info and Debug level logs.
        /// </summary>
        public static LogLevel Filter = LogLevel.Debug;
        
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

        /// <summary>
        /// Logs a message at a specific level.
        /// </summary>
        /// <param name="level">The LogLevel at which this log should be output.</param>
        /// <param name="caller">The calling object or null.</param>
        /// <param name="message">The object to log.</param>
        /// <param name="replacements">String replacements.</param>
        private static void Out(
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

            // call all ILogTargets
            for (int i = 0, len = _targets.Count; i < len; i++)
            {
                _targets[i].OnLog(level, caller, message.ToString());
            }
        }
    }
}