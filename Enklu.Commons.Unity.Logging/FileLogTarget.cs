﻿using System;
using System.IO;

namespace Enklu.Commons.Unity.Logging
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

        /// <inheritdoc />
        public LogLevel Filter { get; set; }

        /// <summary>
        /// Creates a new FileLogTarget.
        /// </summary>
        /// <param name="formatter">The ILogFormatter to use.</param>
        /// <param name="filePath">Relative or absolute path to a log file.</param>
        public FileLogTarget(ILogFormatter formatter, string filePath)
        {
            _formatter = formatter;
            
            // make sure directory exists
            var directoryName = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directoryName)
                && !Directory.Exists(directoryName))
            {
                try
                {
                    Directory.CreateDirectory(directoryName);
                }
                catch (Exception exception)
                {
                    Log.Error(this,
                        "Could not create FileLogTarget directory : {0}.",
                        exception);

                    return;
                }
            }

            // copy previous one
            if (File.Exists(filePath))
            {
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                var extension = Path.GetExtension(filePath);
                var copyPath = Path.Combine(
                    directoryName,
                    string.Format("{0}.previous{1}", fileName, extension));
                File.Copy(filePath, copyPath, true);
            }

            // create new one
            try
            {
                _writer = File.CreateText(filePath);
            }
            catch (Exception exception)
            {
                Log.Error(this,
                    "Could not create FileLogTarget file : {0}.",
                    exception);
            }
            
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
        public void OnLog(LogLevel level, object caller, string message, object meta)
        {
            if (level < Filter)
            {
                return;
            }

            try
            {
                _writer?.Write(_formatter.Format(level, caller, message));
            }
            catch
            {
                //
            }
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