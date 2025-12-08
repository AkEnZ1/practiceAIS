using System;
using Shared.Interfaces;

namespace Infrastructure.Logger
{
    /// <summary>
    /// Простой логгер, выводящий сообщения в консоль
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        private readonly string _context;

        public ConsoleLogger(string context = "Application")
        {
            _context = context;
        }

        public void LogInfo(string message)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [INFO] [{_context}] {message}");
        }

        public void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [WARN] [{_context}] {message}");
            Console.ResetColor();
        }

        public void LogError(string message, Exception ex = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [ERROR] [{_context}] {message}");
            if (ex != null)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
            Console.ResetColor();
        }

        public void LogDebug(string message)
        {
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [DEBUG] [{_context}] {message}");
            Console.ResetColor();
#endif
        }
    }
}