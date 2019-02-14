using System;

namespace CreateAR.Commons.Unity.Logging
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
        /// Generates a dump of logs.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="formatter"></param>
        /// <returns></returns>
        string GenerateDump(LogDumpOptions options = LogDumpOptions.None);
    }
}