using System;

namespace Harbour.Utils
{
    /// <summary>
    /// 文件记录器选项
    /// </summary>
    public class FileLoggerOptions
    {
        /// <summary>
        /// 过滤器
        /// </summary>
        public Func<string, LogLevel, bool> Filter { get; set; }

        /// <summary>
        /// 日志格式化
        /// </summary>
        public Func<LogLevel, string, string, Exception, string> LogFormatter { get; set; }
        /// <summary>
        /// 路径
        /// </summary>

        public string Path { get; set; }
    }
}
