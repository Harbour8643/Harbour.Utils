using Newtonsoft.Json;
using System;
using System.Web;

namespace Harbour.Web
{
    /// <summary>
    /// Cookie帮助类
    /// </summary>
    public class CookieHelper
    {
        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="CookieName">Cookie名</param>
        public static void DeleteCookie(string CookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="CookieName">Cookie名</param>
        /// <returns></returns>
        public static V GetCookieValue<V>(string CookieName)
        {
            string value = string.Empty;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
                value = cookie.Value;
            value = HttpContext.Current.Server.UrlDecode(value);
            if (typeof(V) == typeof(string))
            {
                return (V)Convert.ChangeType(value, typeof(V));
            }
            else
            {
                //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                return JsonConvert.DeserializeObject<V>(value);
            }
        }

        /// <summary>
        /// 设置指定名称的Cookie的值
        /// </summary>
        /// <param name="name">Cookie名称</param>
        /// <param name="value">值</param>
        /// <param name="expiresMin">过期时间(分钟)</param>
        public static void SetCookieValue<V>(string name, V value, int expiresMin = 0)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie == null)
                cookie = new HttpCookie(name);

            string setVal = string.Empty;
            if (typeof(V) == typeof(string))
            {
                setVal = value.ToString();
            }
            else
            {
                //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                setVal = JsonConvert.SerializeObject(value);
            }
            setVal = HttpContext.Current.Server.UrlEncode(setVal);
            cookie.Values.Set(name, setVal);
            if (expiresMin > 0)
                cookie.Expires = DateTime.Now.AddMinutes(expiresMin);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
    }
}
