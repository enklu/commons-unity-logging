using System;
using System.IO;

namespace CreateAR.Commons.Unity.Logging
{
    /// <summary>
    /// Forwards logs to a file.
    /// </summary>
    public class FileLogTarget : IDisposable, ILogTarget
    {
        /// <summary>
        /// Formats logs.
        /// </summary>
        private readonly ILogFormatter _formatter;

        /// <summary>
        /// Writes to the text file.
        /// </summary>
        private readonly StreamWriter _writer;

        /// <summary>
        /// Creates a new FileLogTarget.
        /// </summary>
        /// <param name="formatter">The ILogFormatter to use.</param>
        /// <param name="filePath">Relative or absolute path to a log file.</param>
        public FileLogTarget(ILogFormatter formatter, string filePath)
        {
            _formatter = formatter;
            
            var directoryName = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directoryName)
                && !Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            
            _writer = File.CreateText(filePath);

            _writer.AutoFlush = true;
            _writer.WriteLine(
                "Created on {0:dd:MM:yyyy} at {0:HH:mm:ss.fff}.\n",
                DateTime.Now);
        }

        /// <summary>
        /// When called, logs to the file.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="caller"></param>
        /// <param name="message"></param>
        public void OnLog(LogLevel level, object caller, string message)
        {
            _writer?.Write(_formatter.Format(level, caller, message));
        }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// IDisposable pattern.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _writer?.Dispose();
            }
        }

        /// <summary>
        /// IDisposable pattern.
        /// </summary>
        ~FileLogTarget()
        {
            Dispose(false);
        }
    }
}