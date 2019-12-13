using System;
using System.Runtime.CompilerServices;

namespace M2MCommunication.Core.Interfaces
{
    public interface ILogger
    {
        void LogInfo(string message, [CallerMemberName] string callerName = "", [CallerFilePath] string callerPath = "");
        void LogWarning(string message, [CallerMemberName] string callerName = "", [CallerFilePath] string callerPath = "");
        void LogWarning(Exception exception, string message, [CallerMemberName] string callerName = "", [CallerFilePath] string callerPath = "");
        void LogError(string message, [CallerMemberName] string callerName = "", [CallerFilePath] string callerPath = "");
        void LogError(Exception exception, string message, [CallerMemberName] string callerName = "", [CallerFilePath] string callerPath = "");
    }
}
