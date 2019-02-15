using System;
using System.Text;
using System.Threading;

namespace CreateAR.Commons.Unity.Logging
{
    /// <summary>
    /// Keeps in memory history.
    /// </summary>
    public class LogHistory : ILogTarget, ILogHistory
    {
        /// <summary>
        /// Record of a log.
        /// </summary>
        private class LogRecord
        {
            /// <summary>
            /// The log level.
            /// </summary>
            public LogLevel Level;

            /// <summary>
            /// The log.
            /// </summary>
            public string FormattedLog;
        }

        /// <summary>
        /// Formats logs.
        /// </summary>
        private readonly ILogFormatter _formatter;

        /// <summary>
        /// How many logs to keep.
        /// </summary>
        private int _size;

        /// <summary>
        /// Ring buffer of logs.
        /// </summary>
        private LogRecord[] _logs;

        /// <summary>
        /// Index into log buffer.
        /// </summary>
        private int _index;

        /// <inheritdoc cref="ILogHistory" />
        public LogLevel Filter { get; set; }

        /// <inheritdoc />
        public int Size
        {
            get => _size;
            set
            {
                _size = value;

                var destination = new LogRecord[_size];
                
                // copy
                if (null != _logs)
                {
                    var len = Math.Min(_logs.Length, value);
                    Array.Copy(_logs, 0, destination, 0, len);

                    for (var i = len; i < value; i++)
                    {
                        destination[i] = _logs[i];
                    }
                }

                _logs = destination;

                for (var i = 0; i < _size; i++)
                {
                    _logs[i] = _logs[i] ?? new LogRecord();
                }
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public LogHistory(ILogFormatter formatter)
        {
            _formatter = formatter;

            Size = 100;
        }

        /// <inheritdoc />
        public void OnLog(LogLevel level, object caller, string message)
        {
            if (level < Filter)
            {
                return;
            }

            var record = _logs[Interlocked.Increment(ref _index) % _size];
            record.Level = level;
            record.FormattedLog = _formatter.Format(level, caller, message);
        }

        /// <inheritdoc />
        public string GenerateDump(LogDumpOptions options = LogDumpOptions.None)
        {
            return GenerateDump(Filter, options);
        }

        /// <inheritdoc />
        public string GenerateDump(
            LogLevel filter,
            LogDumpOptions options = LogDumpOptions.None)
        {
            // trivial case: no logs yet
            if (0 == _index)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            for (var i = Math.Max(0, _index - 1); i > Math.Max(0, _index - _size + 1); i--)
            {
                var index = i % _size;
                var record = _logs[index];

                // filter
                if (record.Level < filter)
                {
                    continue;
                }

                // prepend
                if ((options & LogDumpOptions.Reverse) == 0)
                {
                    builder.Insert(0, record.FormattedLog + "\n");
                }
                // append
                else
                {
                    builder.AppendLine(record.FormattedLog);
                }
            }

            return builder.ToString();
        }
    }
}