using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Harbour.Web
{
    /// <summary>
    /// Http帮助类
    /// </summary>
    public static class HttpHelper
    {
        #region 私有静态属性
        //浏览器列表
        private static string[] _browserlist = new string[] { "ie", "chrome", "mozilla", "netscape", "firefox", "opera", "konqueror" };
        //搜索引擎列表
        private static string[] _searchenginelist = new string[] { "baidu", "google", "360", "sogou", "bing", "msn", "sohu", "soso", "sina", "163", "yahoo", "jikeu" };
        //meta正则表达式
        private static Regex _metaregex = new Regex("<meta([^<]*)charset=([^<]*)[\"']", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        #endregion

        #region HTML和URL编码
        //为了解决带html标签的文本在IIS上被拦截不通过的问题。
        /// <summary>
        /// HTML解码
        /// </summary>
        /// <returns></returns>
        public static string HtmlDecode(string s)
        {
            return HttpUtility.HtmlDecode(s);
        }
        /// <summary>
        /// HTML编码
        /// </summary>
        /// <returns></returns>
        public static string HtmlEncode(string s)
        {
            return HttpUtility.HtmlEncode(s);
        }

        //为了解决url传输的问题，eg：url中空格，
        /// <summary>
        /// URL解码
        /// </summary>
        /// <returns></returns>
        public static string UrlDecode(string s)
        {
            return HttpUtility.UrlDecode(s);
        }
        /// <summary>
        /// URL编码
        /// </summary>
        /// <returns></returns>
        public static string UrlEncode(string s)
        {
            return HttpUtility.UrlEncode(s);
        }
        #endregion

        #region 请求判断
        /// <summary>
        /// 是否是get请求
        /// </summary>
        /// <returns></returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod == "GET";
        }
        /// <summary>
        /// 是否是post请求
        /// </summary>
        /// <returns></returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod == "POST";
        }
        /// <summary>
        /// 是否是Ajax请求
        /// </summary>
        /// <returns></returns>
        public static bool IsAjax()
        {
            return HttpContext.Current.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
        /// <summary>
        /// 判断是否是浏览器请求
        /// </summary>
        /// <returns></returns>
        public static bool IsBrowser()
        {
            string name = GetBrowserName();
            foreach (string item in _browserlist)
            {
                if (name.Contains(item))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 是否是移动设备请求
        /// </summary>
        /// <returns></returns>
        public static bool IsMobile()
        {
            if (HttpContext.Current.Request.Browser.IsMobileDevice)
                return true;

            bool isTablet = false;
            if (bool.TryParse(HttpContext.Current.Request.Browser["IsTablet"], out isTablet) && isTablet)
                return true;
            string strUserAgent = HttpContext.Current.Request.UserAgent.ToString().ToLower();
            if (strUserAgent != null)
            {
                if (strUserAgent.Contains("iphone") || strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
                    strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") || strUserAgent.Contains("palm"))
                {
                    return true;
                }
            }
            return false;////手机调试的时候将其改为  true
        }
        /// <summary>
        /// 判断是否是搜索引擎爬虫请求
        /// </summary>
        /// <returns></returns>
        public static bool IsCrawler()
        {
            bool result = HttpContext.Current.Request.Browser.Crawler;
            if (!result)
            {
                string referrer = GetUrlReferrer();
                if (referrer.Length > 0)
                {
                    foreach (string item in _searchenginelist)
                    {
                        if (referrer.Contains(item))
                            return true;
                    }
                }
            }
            return result;
        }
        #endregion

        #region 获取请求参数

        /// <summary>
        ///Url传参 获得查询字符串中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetQueryString(string key, string defaultValue = null)
        {
            string value = HttpContext.Current.Request.QueryString[key];
            if (!string.IsNullOrWhiteSpace(value))
                return value;
            else
                return defaultValue;
        }
        /// <summary>
        /// Url传参 获得查询字符串中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetQueryInt(string key, int defaultValue = 0)
        {
            return GetQueryString(key).TryToInt(defaultValue);
        }

        /// <summary>
        /// Post表单 获得表单中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetFormString(string key, string defaultValue = null)
        {
            string value = HttpContext.Current.Request.Form[key];
            if (!string.IsNullOrWhiteSpace(value))
                return value;
            else
                return defaultValue;
        }
        /// <summary>
        /// Post表单 获得表单中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetFormInt(string key, int defaultValue = 0)
        {
            return GetFormString(key).TryToInt(defaultValue);
        }

        /// <summary>
        /// 获得请求中的值(Post请求数据优先，Get优先级低)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetRequestString(string key, string defaultValue = null)
        {
            string value = HttpContext.Current.Request[key];
            if (!string.IsNullOrWhiteSpace(value))
                return value;
            else
                return defaultValue;
            //if (HttpContext.Current.Request.Form[key] != null)
            //    return GetFormString(key, defaultValue);
            //else
            //    return GetQueryString(key, defaultValue);
        }
        /// <summary>
        /// 获得请求中的值(Post请求数据优先，Get优先级低)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetRequestInt(string key, int defaultValue = 0)
        {
            return GetRequestString(key).TryToInt(defaultValue);
        }

        #endregion

        #region 客户端信息

        /// <summary>
        /// 获得请求的主机部分
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }

        /// <summary>
        /// 获得上次请求的url
        /// </summary>
        /// <returns></returns>
        public static string GetUrlReferrer()
        {
            Uri uri = HttpContext.Current.Request.UrlReferrer;
            if (uri == null)
                return null;

            return uri.ToString();
        }
        /// <summary>
        /// 获得请求的url
        /// </summary>
        /// <returns></returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }
        /// <summary>
        /// 获得请求的原始url
        /// </summary>
        /// <returns></returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        /// <summary>
        /// 获得请求的ip
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string ip = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            else
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

            //IP正则表达式
            Regex _ipregex = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
            bool isIP = _ipregex.IsMatch(ip);
            if (string.IsNullOrEmpty(ip) || !isIP)
                ip = "127.0.0.1";
            return ip;
        }
        /// <summary>
        /// 域名
        /// </summary>
        public static string Domain
        {
            get
            {
                if (((HttpContext.Current.Request.ServerVariables["SERVER_PORT"] == null) || (HttpContext.Current.Request.ServerVariables["SERVER_PORT"] == "")) || (HttpContext.Current.Request.ServerVariables["SERVER_PORT"] == "80"))
                {
                    return HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                }
                return (HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + ":" + HttpContext.Current.Request.ServerVariables["SERVER_PORT"]);
            }
        }

        /// <summary>
        /// 获得请求的浏览器类型
        /// </summary>
        /// <returns></returns>
        public static string GetBrowserType()
        {
            string type = HttpContext.Current.Request.Browser.Type;
            if (string.IsNullOrEmpty(type) || type == "unknown")
                return null;

            return type.ToLower();
        }
        /// <summary>
        /// 获得请求的浏览器名称
        /// </summary>
        /// <returns></returns>
        public static string GetBrowserName()
        {
            string name = HttpContext.Current.Request.Browser.Browser;
            if (string.IsNullOrEmpty(name) || name == "unknown")
                return null;

            return name.ToLower();
        }
        /// <summary>
        /// 获得请求的浏览器版本
        /// </summary>
        /// <returns></returns>
        public static string GetBrowserVersion()
        {
            string version = HttpContext.Current.Request.Browser.Version;
            if (string.IsNullOrEmpty(version) || version == "unknown")
                return null;

            return version;
        }

        /// <summary>
        /// 获得请求客户端的操作系统类型
        /// </summary>
        /// <returns></returns>
        public static string GetOSType()
        {
            string userAgent = HttpContext.Current.Request.UserAgent;
            if (userAgent == null)
                return null;

            string type = null;
            if (userAgent.Contains("NT 6.1"))
                type = "Windows 7";
            else if (userAgent.Contains("NT 5.1"))
                type = "Windows XP";
            else if (userAgent.Contains("NT 6.2"))
                type = "Windows 8";
            else if (userAgent.Contains("android"))
                type = "Android";
            else if (userAgent.Contains("iphone"))
                type = "IPhone";
            else if (userAgent.Contains("Mac"))
                type = "Mac";
            else if (userAgent.Contains("NT 6.0"))
                type = "Windows Vista";
            else if (userAgent.Contains("NT 5.2"))
                type = "Windows 2003";
            else if (userAgent.Contains("NT 5.0"))
                type = "Windows 2000";
            else if (userAgent.Contains("98"))
                type = "Windows 98";
            else if (userAgent.Contains("95"))
                type = "Windows 95";
            else if (userAgent.Contains("Me"))
                type = "Windows Me";
            else if (userAgent.Contains("NT 4"))
                type = "Windows NT4";
            else if (userAgent.Contains("Unix"))
                type = "UNIX";
            else if (userAgent.Contains("Linux"))
                type = "Linux";
            else if (userAgent.Contains("SunOS"))
                type = "SunOS";

            return type;
        }
        /// <summary>
        /// 获得请求客户端的操作系统名称
        /// </summary>
        /// <returns></returns>
        public static string GetOSName()
        {
            string name = HttpContext.Current.Request.Browser.Platform;
            if (string.IsNullOrEmpty(name))
                return null;

            return name;
        }

        #endregion

        /// <summary>
        /// 强转成int 如果失败返回 defaultValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="defaultValue">默认值:0</param>
        /// <returns></returns>
        static int TryToInt(this object thisValue, int defaultValue = 0)
        {
            int reval = 0;
            if (thisValue != null && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return defaultValue;
        }
    }
}
