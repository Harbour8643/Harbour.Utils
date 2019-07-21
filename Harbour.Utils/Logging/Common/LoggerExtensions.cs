using System;

namespace Harbour.Utils
{
    /// <summary>
    /// 日志记录器的扩展方法
    /// </summary>
    public static class LoggerExtensions
    {
        private static readonly Func<string, Exception, string> _messageFormatter = (message, exception) => message;

        //------------------------------------------DEBUG------------------------------------------//
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="message">日志信息</param>
        public static void LogDebug(this ILogger logger, string message)
        {
            logger.Log(LogLevel.DEBUG, message);
        }
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="exception">异常信息</param>
        /// <param name="message">日志信息</param>
        public static void LogDebug(this ILogger logger, Exception exception, string message = null)
        {
            logger.Log(LogLevel.DEBUG, exception, message);
        }

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="group">分组</param>
        /// <param name="message">日志信息</param>
        public static void LogDebug(this ILogger logger, string group, string message)
        {
            logger.Log(LogLevel.DEBUG, group, message);
        }
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="group">分组</param>
        /// <param name="exception">异常信息</param>
        /// <param name="message">日志信息</param>
        public static void LogDebug(this ILogger logger, string group, Exception exception, string message = null)
        {
            logger.Log(LogLevel.DEBUG, group, exception, message);
        }

        //------------------------------------------INFO------------------------------------------//
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="message">日志信息</param>
        public static void LogInfo(this ILogger logger, string message)
        {
            logger.Log(LogLevel.INFO, message);
        }
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="exception">异常信息</param>
        /// <param name="message">日志信息</param>
        public static void LogInfo(this ILogger logger, Exception exception, string message = null)
        {
            logger.Log(LogLevel.INFO, exception, message);
        }

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="group">分组</param>
        /// <param name="message">日志信息</param>
        public static void LogInfo(this ILogger logger, string group, string message)
        {
            logger.Log(LogLevel.INFO, group, message);
        }
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="group">分组</param>
        /// <param name="exception">异常信息</param>
        /// <param name="message">日志信息</param>
        public static void LogInfo(this ILogger logger, string group, Exception exception, string message = null)
        {
            logger.Log(LogLevel.INFO, group, exception, message);
        }

        //------------------------------------------WARNING------------------------------------------//
        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="message">日志信息</param>
        public static void LogWarning(this ILogger logger, string message)
        {
            logger.Log(LogLevel.WARNIMG, message);
        }
        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="exception">异常信息</param>
        /// <param name="message">日志信息</param>
        public static void LogWarning(this ILogger logger, Exception exception, string message = null)
        {
            logger.Log(LogLevel.WARNIMG, exception, message);
        }

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="group">分组</param>
        /// <param name="message">日志信息</param>
        public static void LogWarning(this ILogger logger, string group, string message)
        {
            logger.Log(LogLevel.WARNIMG, group, message);
        }
        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="group">分组</param>
        /// <param name="exception">异常信息</param>
        /// <param name="message">日志信息</param>
        public static void LogWarning(this ILogger logger, string group, Exception exception, string message = null)
        {
            logger.Log(LogLevel.WARNIMG, group, exception, message);
        }

        //------------------------------------------ERROR------------------------------------------//
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="message">日志信息</param>
        public static void LogError(this ILogger logger, string message)
        {
            logger.Log(LogLevel.WARNIMG, message);
        }
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="exception">异常信息</param>
        /// <param name="message">日志信息</param>
        public static void LogError(this ILogger logger, Exception exception, string message = null)
        {
            logger.Log(LogLevel.WARNIMG, exception, message);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="group">分组</param>
        /// <param name="message">日志信息</param>
        public static void LogError(this ILogger logger, string group, string message)
        {
            logger.Log(LogLevel.WARNIMG, group, message);
        }
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="group">分组</param>
        /// <param name="exception">异常信息</param>
        /// <param name="message">日志信息</param>
        public static void LogError(this ILogger logger, string group, Exception exception, string message = null)
        {
            logger.Log(LogLevel.WARNIMG, group, exception, message);
        }

        //------------------------------------------CRITICAL------------------------------------------//

        /// <summary>
        /// Critical
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="message">日志信息</param>
        public static void LogCritical(this ILogger logger, string message)
        {
            logger.Log(LogLevel.CRITICAL, message);
        }
        /// <summary>
        /// Critical
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="exception">异常信息</param>
        /// <param name="message">日志信息</param>
        public static void LogCritical(this ILogger logger, Exception exception, string message = null)
        {
            logger.Log(LogLevel.CRITICAL, exception, message);
        }

        /// <summary>
        /// Critical
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="group">分组</param>
        /// <param name="message">日志信息</param>
        public static void LogCritical(this ILogger logger, string group, string message)
        {
            logger.Log(LogLevel.CRITICAL, group, message);
        }
        /// <summary>
        /// Critical
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="group">分组</param>
        /// <param name="exception">异常信息</param>
        /// <param name="message">日志信息</param>
        public static void LogCritical(this ILogger logger, string group, Exception exception, string message = null)
        {
            logger.Log(LogLevel.CRITICAL, group, exception, message);
        }


        /// <summary>
        /// Log
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="logLevel">日志等级</param>
        /// <param name="message">日志信息</param>
        public static void Log(this ILogger logger, LogLevel logLevel, string message)
        {
            logger.Log(logLevel, null, null, message);
        }
        /// <summary>
        /// Log
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="logLevel">日志等级</param>
        /// <param name="group">分组</param>
        /// <param name="message">日志信息</param>
        public static void Log(this ILogger logger, LogLevel logLevel, string group, string message)
        {
            logger.Log(logLevel, group, null, message);
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="logLevel">日志等级</param>
        /// <param name="exception">异常信息</param>
        /// <param name="message">日志信息</param>
        public static void Log(this ILogger logger, LogLevel logLevel, Exception exception, string message)
        {
            logger.Log(logLevel, null, exception, message);
        }
        /// <summary>
        /// Log
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="logLevel">日志等级</param>
        /// <param name="group">分组</param>
        /// <param name="exception">异常信息</param>
        /// <param name="message">日志信息</param>
        public static void Log(this ILogger logger, LogLevel logLevel, string group, Exception exception, string message)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(logLevel, group, message, exception, _messageFormatter);
        }
    }
}
