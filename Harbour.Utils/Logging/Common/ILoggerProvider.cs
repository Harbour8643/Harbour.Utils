using System;

namespace Harbour.Utils
{
    /// <summary>
    /// 表示可以创建的实例的类型<see cref="ILogger"/>.
    /// </summary>
    public interface ILoggerProvider : IDisposable
    {
        /// <summary>
        /// 创建新<see cref="ILogger"/> 实例.
        /// </summary>
        /// <param name="categoryName">记录器生成的消息的类别名称</param>
        /// <returns></returns>
        ILogger CreateLogger(string categoryName);
    }
}
