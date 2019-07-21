namespace Harbour.Utils
{
    /// <summary>
    /// 文件日志提供程序
    /// </summary>
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly FileLoggerWriter _fileLoggerWriter;
        private readonly FileLoggerOptions _options;

        /// <summary>
        /// 文件日志提供程序
        /// </summary>
        /// <param name="options"></param>
        public FileLoggerProvider(FileLoggerOptions options = null)
        {
            _options = options ?? new FileLoggerOptions();
            _fileLoggerWriter = new FileLoggerWriter(_options);
        }
        /// <summary>
        /// 创建Logger
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string name = "defaultLogger")
        {
            return new FileLogger(name, _fileLoggerWriter);
        }
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
        }
    }
}
