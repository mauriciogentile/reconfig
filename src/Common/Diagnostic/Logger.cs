using System;
using NLog;

namespace Reconfig.Common.Diagnostic
{
    public class Logger : ILogger
    {
        static readonly NLog.Logger InternalLogger;

        static Logger()
        {
            InternalLogger = LogManager.GetCurrentClassLogger();
        }

        public void Debug(string message)
        {
            InternalLogger.Debug(message);
        }

        public void Info(string message, Exception exc = null)
        {
            if (exc == null)
            {
                InternalLogger.Info(message);
            }
            else
            {
                InternalLogger.InfoException(message, exc);
            }
        }

        public void Fatal(string message, Exception exc = null)
        {
            if (exc == null)
            {
                InternalLogger.Fatal(message);
            }
            else
            {
                InternalLogger.FatalException(message, exc);
            }
        }

        public void Error(string message, Exception exc = null)
        {
            if (exc == null)
            {
                InternalLogger.Error(message);
            }
            else
            {
                InternalLogger.ErrorException(message, exc);
            }
        }

        public void Warning(string message, Exception exc = null)
        {
            if (exc == null)
            {
                InternalLogger.Warn(message);
            }
            else
            {
                InternalLogger.WarnException(message, exc);
            }
        }
    }
}
