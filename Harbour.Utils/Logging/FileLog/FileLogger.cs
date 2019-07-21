using System;

namespace Harbour.Utils
{
    internal partial class FileLogger : ILogger
    {
        private readonly FileLoggerWriter _fileLoggerWriter;
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly string _name;

        public FileLogger(string name, FileLoggerWriter fileLoggerWriter)
        {
            _name = string.IsNullOrEmpty(name) ? nameof(FileLogger) : name;
            _filter = fileLoggerWriter.Options.Filter ?? ((category, logLevel) => true);
            _fileLoggerWriter = fileLoggerWriter;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _filter(_name, logLevel);
        }

        public void Log<TState>(LogLevel logLevel, string group, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            string message = formatter(state, exception);

            if (string.IsNullOrEmpty(message) && exception == null)
                return;

            _fileLoggerWriter.WriteLine(logLevel, group, message, exception);
        }
    }
}
