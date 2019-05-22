using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace Harbour.Utils
{
    /// <summary>
    /// 请求参数类
    /// </summary>
    public class HttpParam
    {
        /// <summary>
        /// GET/POST
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// Url地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// HTTP 头集合
        /// </summary>
        public NameValueCollection CustomHeaders { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        public NameValueCollection RequestParameters { get; set; }
        /// <summary>
        /// 请求超时时间。单位：秒。默认值100秒。
        /// </summary>
        public int TimeOut { get; set; } = 100 * 1000;
        /// <summary>
        /// 编码方式。默认值：Encoding.UTF8
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        /// <summary>
        /// Cookie容器
        /// </summary>
        public CookieContainer CookieContainer { get; set; }
        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; set; } = "application/x-www-form-urlencoded";
        /// <summary>
        /// 客户端信息
        /// </summary>
        public string UserAgent { get; set; } = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Maxthon/4.1.2.4000 Chrome/26.0.1410.43 Safari/537.1";

        /// <summary>
        /// FileStream
        /// </summary>
        public List<UploadFileParams> UploadFiles { get; set; } = new List<UploadFileParams>();
    }
    /// <summary>
    /// 上传文件类
    /// </summary>
    public class UploadFileParams
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 上传文件的流
        /// </summary>
        public Stream FileStream { get; set; }
    }
}
