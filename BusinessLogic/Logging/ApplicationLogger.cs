using Serilog;
using Serilog.Events;
using System;

namespace BusinessLogic.Logging
{
    public static class ApplicationLogger
    {
        private static ILogger _logger;

        static ApplicationLogger()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(
                    path: "logs/app-.log",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }

        public static void Information(string message, params object[] args)
        {
            _logger.Information(message, args);
        }

        public static void Warning(string message, params object[] args)
        {
            _logger.Warning(message, args);
        }

        public static void Error(Exception exception, string message, params object[] args)
        {
            _logger.Error(exception, message, args);
        }

        public static void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        public static void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }
    }
}