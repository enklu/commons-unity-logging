using UnityEngine;

namespace CreateAR.Commons.Unity.Logging
{
    public class UnityLogTarget : ILogTarget
    {
        private readonly ILogFormatter _formatter;

        public UnityLogTarget(ILogFormatter formatter)
        {
            _formatter = formatter;
        }

        public void OnLog(LogLevel level, object caller, string message)
        {
            switch (level)
            {
                case LogLevel.Debug:
                {
                    Debug.Log(_formatter.Format(level, caller, message));
                    break;
                }
            }
        }
    }
}
