using System;

namespace Shared.Interfaces
{
    /// <summary>
    /// Интерфейс для логирования сообщений
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Логирует информационное сообщение
        /// </summary>
        void LogInfo(string message);

        /// <summary>
        /// Логирует предупреждение
        /// </summary>
        void LogWarning(string message);

        /// <summary>
        /// Логирует ошибку
        /// </summary>
        void LogError(string message, Exception ex = null);

        /// <summary>
        /// Логирует отладочную информацию
        /// </summary>
        void LogDebug(string message);
    }
}