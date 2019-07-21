using System;

namespace Harbour.Utils
{
    /// <summary>
    /// 表示用于执行日志记录的类型
    /// </summary>
    /// <remarks>将大多数日志记录模式聚合到单个方法</remarks>
    public interface ILogger
    {
        /// <summary>
        /// 写入日志项
        /// </summary>
        /// <param name="logLevel">将在此级别上写入条目</param>
        /// <param name="group">日志分组</param>
        /// <param name="state">要写入的条目。也可以是对象</param>
        /// <param name="exception">与此条目相关的异常</param>
        /// <param name="formatter"></param>
        void Log<TState>(LogLevel logLevel, string group, TState state, Exception exception, Func<TState, Exception, string> formatter);

        /// <summary>
        /// 监测日志级别是否可用
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        bool IsEnabled(LogLevel logLevel);
    }
}
