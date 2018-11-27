using UnityEngine;

namespace CreateAR.Commons.Unity.Logging
{
    /// <summary>
    /// Targets Unity logging methods.
    /// </summary>
    public class UnityLogTarget : ILogTarget
    {
        /// <summary>
        /// Log formatter.
        /// </summary>
        private readonly ILogFormatter _formatter;

        /// <inheritdoc />
        public LogLevel Filter { get; set; }

        /// <summary>
        /// Creates a new UnityLogTarget.
        /// </summary>
        /// <param name="formatter">Formats logs</param>
        public UnityLogTarget(ILogFormatter formatter)
        {
            _formatter = formatter;
        }

        /// <inheritdoc cref="ILogTarget"/>
        public void OnLog(LogLevel level, object caller, string message)
        {
            if (level < Filter)
            {
                return;
            }

            var formattedMessage = _formatter.Format(level, caller, message);

            switch (level)
            {
                case LogLevel.Info:
                case LogLevel.Debug:
                {
                    Debug.Log(formattedMessage);
                    break;
                }
                case LogLevel.Warning:
                {
                    Debug.LogWarning(formattedMessage);
                    break;
                }
                case LogLevel.Error:
                case LogLevel.Fatal:
                {
                    Debug.LogError(formattedMessage);
                    break;
                }
            }
        }
    }
}
