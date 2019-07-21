using System;
using System.IO;
using System.Text;

namespace Harbour.Utils
{
    internal class FileLoggerWriter
    {
        private readonly string _logDir;
        public FileLoggerOptions Options { get; }

        public FileLoggerWriter(FileLoggerOptions options)
        {
            string defaultDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Options = options;
            _logDir = Options.Path ?? Path.Combine(defaultDirectory, "Logs");

            if (options.LogFormatter == null)
                options.LogFormatter = LogFormatter;
        }

        public void WriteLine(LogLevel level, string group, string message, Exception exception)
        {
            string fileName = DateTime.Now.ToString("yyyyMMdd");
            if (!string.IsNullOrEmpty(group))
                fileName += "--" + group;
            string log = Options.LogFormatter(level, group, message, exception);
            try
            {
                WriteLog(fileName, log);
            }
            catch (DirectoryNotFoundException)
            {
                CreateLogDir();
                WriteLog(fileName, log);
            }
            catch (Exception)
            {
            }
        }

        private void CreateLogDir()
        {
            if (!Directory.Exists(_logDir))
                Directory.CreateDirectory(_logDir);
        }
        private void WriteLog(string fileName, string log)
        {
            File.AppendAllText(Path.Combine(_logDir, $"{fileName}.txt"), log);
        }
        private string LogFormatter(LogLevel level, string group, string message, Exception exception)
        {
            var logBuilder = new StringBuilder();
            logBuilder.Append($"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}] ");
            logBuilder.Append(level.ToString().PadRight(8));
            logBuilder.Append(message);
            if (exception != null)
                logBuilder.Append("\r\n   " + exception.ToString());
            logBuilder.AppendLine();

            return logBuilder.ToString();
        }
    }
}
