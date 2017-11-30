using System;
using System.Configuration;
using System.Web.Configuration;

namespace Harbour.Utils
{
	/// <summary>
	/// web.config操作类
	/// </summary>
	public sealed class ConfigHelper
	{
        /// <summary>
        /// 获得Web.config ConnectionString 里的配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ToString();
        }


        /// <summary>
        /// 将配置信息转化为字符串
        /// </summary>
        /// <param name="key">AppSettings中的key</param>
        /// <param name="defaultValue">默认返回值</param>
        /// <returns>找到与key相应的值，则返回该值，否则返回默认值</returns>
        public static string GetAppString(string key, string defaultValue = null)
        {
            string keyValue = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(keyValue))
            {
                return keyValue;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将配置信息转化为整型
        /// </summary>
        /// <param name="key">AppSettings中的key</param>
        /// <param name="defaultValue">默认返回值</param
        /// <returns>找到与key相应的值，则返回该值，否则返回默认值</returns>
        public static int GetAppInt(string key, int defaultValue = 0)
        {
            string keyValue = ConfigurationManager.AppSettings[key];
            int tempValue;
            if (int.TryParse(keyValue, out tempValue))
            {
                return tempValue;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将配置信息转化为布尔型
        /// </summary>
        /// <param name="key">AppSettings中的key</param>
        /// <param name="defaultValue">默认返回值</param
        /// <returns>找到与key相应的值，则返回该值，否则返回默认值</returns>
        public static bool GetAppBool(string key, bool defaultValue = false)
        {
            string keyValue = ConfigurationManager.AppSettings[key];
            bool tempValue;
            if (bool.TryParse(keyValue, out tempValue))
            {
                return tempValue;
            }
            return defaultValue;
        }

        /// <summary>
        /// 设置AppSettings
        /// </summary>
        /// <param name="name">名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool SetAppSetting(string name, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration("~");
                if (config.AppSettings.Settings[name] != null)
                {
                    config.AppSettings.Settings.Remove(name);
                }
                config.AppSettings.Settings.Add(name, value);
                config.Save(ConfigurationSaveMode.Modified);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
