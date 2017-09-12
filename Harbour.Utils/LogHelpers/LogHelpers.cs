﻿using System;
using System.Configuration;
using System.IO;

namespace Harbour.Utils
{

    /// <summary>
    /// 日志帮助类。AppSettings节点可以配置Har.LogHelper.Debug=0、Har.LogHelper.Error=0、Har.LogHelper.Write=0来关闭日志记录。
    /// 如果不传入path参数，默认是在~/Log/下生成日志文件，
    /// 也可以在AppSettings节点配置Har.LogHelper.Path来设置默认日志文件路径，格式：D:\\File\\Log\\。
    /// </summary>
    public class LogHelpers
    {
        private static readonly object Olock = new object();
        private enum LogHelperType
        {
            debug, error, write
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <Param name="content">内容。如需换行可使用：\r\n</Param>
        /// <Param name="filePrefixName"></Param>
        /// <Param name="path">格式：D:\\File\\Logs\\</Param>
        public static void Write(string content, string filePrefixName = null, string path = null)
        {
            Write(LogHelperType.write, content, filePrefixName, path);
        }

        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <Param name="content">内容。如需换行可使用：\r\n</Param>
        /// <Param name="filePrefixName"></Param>
        /// <Param name="path">格式：D:\\File\\Logs\\</Param>
        public static void Debug(string content, string filePrefixName = null, string path = null)
        {
            Write(LogHelperType.debug, content, filePrefixName, path);
        }
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <Param name="content">内容。如需换行可使用：\r\n</Param>
        /// <Param name="filePrefixName"></Param>
        /// <Param name="path">格式：D:\\File\\Logs\\</Param>
        public static void Error(string content, string filePrefixName = null, string path = null)
        {
            Write(LogHelperType.error, content, filePrefixName, path);
        }
        /// <summary>
        /// filePrefixName是文件名前缀，最好用中文，方便在程序Logs文件下查看。
        /// </summary>
        /// <Param name="content">内容。如需换行可使用：\r\n</Param>
        /// <Param name="filePrefixName"></Param>
        /// <Param name="path"></Param>
        /// <Param name="logtype"></Param>
        private static void Write(LogHelperType logtype, string content, string filePrefixName = null, string path = null)
        {
            lock (Olock)
            {
                try
                {
                    switch (logtype)
                    {
                        case LogHelperType.debug:
                            {
                                var dosDebug = ConfigurationManager.AppSettings["Har.LogHelper.Debug"];
                                if (dosDebug != null && dosDebug != "1")
                                    return;
                                else
                                    break;
                            }
                        case LogHelperType.error:
                            {
                                var dosError = ConfigurationManager.AppSettings["Har.LogHelper.Error"];
                                if (dosError != null && dosError != "1")
                                    return;
                                else
                                    break;
                            }
                        case LogHelperType.write:
                            {
                                var dosWrite = ConfigurationManager.AppSettings["Har.LogHelper.Write"];
                                if (dosWrite != null && dosWrite != "1")
                                    return;
                                  else
                                    break;
                            }
                    }

                    #region 日志文件
                    var fileName = filePrefixName + "_" + DateTime.Now.ToString("yyyyMMdd") + logtype.ToString() + ".txt";
                    if (string.IsNullOrWhiteSpace(path))
                    {
                        var dosPath = ConfigurationManager.AppSettings["Har.LogHelper.Path"];
                        if (string.IsNullOrWhiteSpace(dosPath))
                        {
                            path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + fileName;
                        }
                        else
                        {
                            path = dosPath + fileName;
                        }
                    }
                    else
                    {
                        path += fileName;
                    }
                    var di = new DirectoryInfo(path.Replace(fileName, ""));
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    //判断文件大小，需要新开文件
                    using (var fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        var sw = new StreamWriter(fs);
                        sw.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        sw.WriteLine();
                        sw.Write(content);
                        sw.WriteLine();
                        sw.Write("-----------------------------------------------------------------------------");
                        sw.WriteLine();
                        sw.Flush();
                        sw.Close();
                    }
                    #endregion
                }
                catch
                {
                }
            }
        }
    }
}
