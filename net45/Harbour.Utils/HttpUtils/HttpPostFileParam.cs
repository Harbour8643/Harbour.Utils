using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Harbour.Utils
{
    /// <summary>
    /// 
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
        /// 参数类型。可选：Json、Form。默认Json。传入Form则会将new { Key1 = Value1, Key2 = Value2}转换成"key1=value1＆key2=value2"形式。
        /// </summary>
        public HttpParamType PostParamType { get; set; } = HttpParamType.Json;
        /// <summary>
        /// Post参数。
        /// <para>可以传入Json对像：new { Key1 = Value1, Key2 = Value2}</para>
        /// <para>可以传入Json字符串：{"Key1":"Value1","Key2":"Value2"}</para>
        /// <para>可以传入key/value字符串："key1=value1＆key2=value2"</para>
        /// <para>可以传入xml字符串等等</para>
        /// </summary>
        public object PostParam { get; set; }
        /// <summary>
        /// Get参数
        /// <para>可以传入Json对像：new { Key1 = Value1, Key2 = Value2}</para>
        /// <para>可以传入Json字符串：{"Key1":"Value1","Key2":"Value2"}</para>
        /// </summary>
        public object GetParam { get; set; }
        /// <summary>
        /// 请求超时时间。单位：秒。默认值5秒。
        /// </summary>
        public int TimeOut { get; set; } = 5;

        /// <summary>
        /// 编码方式。默认值：Encoding.UTF8
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// Cookie容器
        /// </summary>
        public CookieContainer CookieContainer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Referer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CertPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CertPwd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ContentType { get; set; } = "application/x-www-form-urlencoded";

        /// <summary>
        /// 
        /// </summary>
        public string UserAgent { get; set; } = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Maxthon/4.1.2.4000 Chrome/26.0.1410.43 Safari/537.1";

        /// <summary>
        /// FileStream
        /// </summary>
        public FileStream FileStream { get; set; }
    }

    /// <summary>
    /// HttpParamType
    /// </summary>
    public enum HttpParamType
    {
        /// <summary>
        /// json数据。默认值。
        /// </summary>
        Json,
        /// <summary>
        /// 形如：key=value＆key=value＆key=value
        /// </summary>
        Form
    }
}
