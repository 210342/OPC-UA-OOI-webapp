using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace M2MCommunication.Core
{
    public interface ILogger
    {
        void LogInfo(string message, [CallerMemberName] string callerName = "", [CallerFilePath] string callerPath = "");
        void LogWarning(Exception exception, string message, [CallerMemberName] string callerName = "", [CallerFilePath] string callerPath = "");
        void LogError(Exception exception, string message, [CallerMemberName] string callerName = "", [CallerFilePath] string callerPath = "");
    }
}
