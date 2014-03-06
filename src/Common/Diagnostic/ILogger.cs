using System;

namespace Reconfig.Common.Diagnostic
{
    public interface ILogger
    {
        void Debug(string message);
        void Info(string message, Exception exc = null);
        void Fatal(string message, Exception exc = null);
        void Error(string message, Exception exc = null);
        void Warning(string message, Exception exc = null);
    }
}