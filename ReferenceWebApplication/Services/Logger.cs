using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace ReferenceWebApplication.Services
{
    public class Logger : M2MCommunication.Core.Interfaces.ILogger
    {
        private readonly ILogger<Logger> _logger;

        public Logger(ILogger<Logger> logger)
        {
            _logger = logger;
        }

        public void LogInfo(string message, string callerName = "", string callerPath = "")
        {
            _logger.LogInformation(CombineMessage(callerPath, callerName, message));
        }

        public void LogWarning(string message, string callerName = "", string callerPath = "")
        {
            _logger.LogWarning(CombineMessage(callerPath, callerName, message));
        }

        public void LogWarning(Exception exception, string message, string callerName = "", string callerPath = "")
        {
            _logger.LogWarning(exception, CombineMessage(callerPath, callerName, message));
        }

        public void LogError(string message, string callerName = "", string callerPath = "")
        {
            _logger.LogError(CombineMessage(callerPath, callerName, message));
        }

        public void LogError(Exception exception, string message, string callerName = "", string callerPath = "")
        {
            _logger.LogError(exception, CombineMessage(callerPath, callerName, message));
        }

        private string CombineMessage(string callerPath, string callerName, string message) =>
            $"{Path.GetFileNameWithoutExtension(callerPath)}.{callerName}: {message}";
    }
}
