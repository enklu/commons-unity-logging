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
        /// Log history.
        /// </summary>
        private static readonly LogHistory _history = new LogHistory(new DefaultLogFormatter
        {
            Level = true,
            Timestamp = true,
            ObjectToString = false,
            TypeName = true
        });

        /// <summary>
        /// All log targets.
        /// </summary>
        public static ILogTarget[] Targets => _targets.ToArray();

        /// <summary>
        /// History.
        /// </summary>
        public static ILogHistory History => _history;

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
                null,
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
                null,
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
                null,
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
                null,
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
                null,
                replacements);
        }

        /// <summary>
        /// Logs a message at a specific level.
        /// </summary>
        /// <param name="level">The LogLevel at which this log should be output.</param>
        /// <param name="caller">The calling object or null.</param>
        /// <param name="message">The object to log.</param>
        /// <param name="replacements">String replacements.</param>
        public static void Out(
            LogLevel level,
            object caller,
            object message,
            object meta,
            params object[] replacements)
        {
            // TODO: only do this if there is a target active at this level
            if (replacements.Length > 0)
            {
                message = string.Format(message.ToString(), replacements);
            }

            // history
            _history.OnLog(level, caller, message.ToString(), meta);

            // call all ILogTargets
            for (int i = 0, len = _targets.Count; i < len; i++)
            {
                _targets[i].OnLog(level, caller, message.ToString(), meta);
            }
        }
    }
}