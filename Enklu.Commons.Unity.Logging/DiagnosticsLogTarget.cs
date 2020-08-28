using System.Diagnostics;

namespace Enklu.Commons.Unity.Logging
{
    /// <summary>
    /// Log target for diagnostics.
    /// </summary>
    public class DiagnosticsLogTarget : ILogTarget
    {
        /// <summary>
        /// Log formatter.
        /// </summary>
        private readonly ILogFormatter _formatter;

        /// <inheritdoc />
        public LogLevel Filter { get; set; }

        /// <summary>
        /// Creates a new log target.
        /// </summary>
        /// <param name="formatter">A formatter with which to format logs.</param>
        public DiagnosticsLogTarget(ILogFormatter formatter)
        {
            _formatter = formatter;
        }

        /// <inheritdoc cref="ILogTarget"/>
        public void OnLog(LogLevel level, object caller, string message, object meta)
        {
            if (level < Filter)
            {
                return;
            }

            Debug.WriteLine(_formatter.Format(
                level,
                caller,
                message));
        }
    }
}