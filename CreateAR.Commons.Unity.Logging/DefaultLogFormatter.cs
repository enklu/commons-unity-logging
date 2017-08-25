using System;
using System.Text;

namespace CreateAR.Commons.Unity.Logging
{
    /// <summary>
    /// Provides a default implementation for formatting logs.
    /// </summary>
    public class DefaultLogFormatter : ILogFormatter
    {
        /// <summary>
        /// If true, prepends a timestamp to the log. This defaults to true.
        /// </summary>
        public bool Timestamp { get; set; }

        /// <summary>
        /// Specifies the timestamp format to lof.
        /// </summary>
        public string TimestampFormat { get; set; }

        /// <summary>
        /// If true, prepends the log level to the log. This defaults to true.
        /// </summary>
        public bool Level { get; set; }

        /// <summary>
        /// Creates a new DefaultLogFormatter.
        /// </summary>
        public DefaultLogFormatter()
        {
            Timestamp = true;
            Level = true;
            TimestampFormat = "HH:mm:ss.fff";
        }

        /// <summary>
        /// Formats a log.
        /// </summary>
        /// <param name="level">The level of the log passed in.</param>
        /// <param name="caller">The calling object.</param>
        /// <param name="message">The message to log.</param>
        /// <returns></returns>
        public string Format(LogLevel level, object caller, string message)
        {
            var log = new StringBuilder();

            if (Timestamp)
            {
                log.AppendFormat("[{0}]", DateTime.Now.ToString(TimestampFormat));
            }

            if (Level)
            {
                log.AppendFormat("[{0}]", level);
            }

            if (Level || Timestamp)
            {
                log.Append("\t");
            }

            log.AppendFormat("{0}\n", message);

            return log.ToString();
        }
    }
}