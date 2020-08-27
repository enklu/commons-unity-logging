using System;

namespace Enklu.Commons.Unity.Logging
{
    /// <summary>
    /// Options for log dumps.
    /// </summary>
    [Flags]
    public enum LogDumpOptions
    {
        None = 0x0,
        Reverse = 0x1
    }

    /// <summary>
    /// Describes an object that keeps log history in memory.
    /// </summary>
    public interface ILogHistory
    {
        /// <summary>
        /// Filter for history.
        /// </summary>
        LogLevel Filter { get; set; }

        /// <summary>
        /// The maximum number of logs to save.
        /// </summary>
        int Size { get; set; }

        /// <summary>
        /// Generates a log dump from all logs in history.
        /// </summary>
        /// <param name="options">Any additional options.</param>
        /// <returns>The concatenated log dump.</returns>
        string GenerateDump(LogDumpOptions options = LogDumpOptions.None);

        /// <summary>
        /// Generates a log dump from all logs in history.
        /// </summary>
        /// <param name="filter">The filter to apply.</param>
        /// <param name="options">Any additional options.</param>
        /// <returns>The concatenated log dump.</returns>
        string GenerateDump(LogLevel filter, LogDumpOptions options = LogDumpOptions.None);
    }
}