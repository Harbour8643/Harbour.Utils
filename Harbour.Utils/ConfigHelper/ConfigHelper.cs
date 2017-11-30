using System;
using System.Configuration;
using System.Web.Configuration;

namespace Harbour.Utils
{
	/// <summary>
	/// web.config������
	/// </summary>
	public sealed class ConfigHelper
	{
        /// <summary>
        /// ���Web.config ConnectionString ���������Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ToString();
        }


        /// <summary>
        /// ��������Ϣת��Ϊ�ַ���
        /// </summary>
        /// <param name="key">AppSettings�е�key</param>
        /// <param name="defaultValue">Ĭ�Ϸ���ֵ</param>
        /// <returns>�ҵ���key��Ӧ��ֵ���򷵻ظ�ֵ�����򷵻�Ĭ��ֵ</returns>
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
        /// ��������Ϣת��Ϊ����
        /// </summary>
        /// <param name="key">AppSettings�е�key</param>
        /// <param name="defaultValue">Ĭ�Ϸ���ֵ</param
        /// <returns>�ҵ���key��Ӧ��ֵ���򷵻ظ�ֵ�����򷵻�Ĭ��ֵ</returns>
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
        /// ��������Ϣת��Ϊ������
        /// </summary>
        /// <param name="key">AppSettings�е�key</param>
        /// <param name="defaultValue">Ĭ�Ϸ���ֵ</param
        /// <returns>�ҵ���key��Ӧ��ֵ���򷵻ظ�ֵ�����򷵻�Ĭ��ֵ</returns>
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
        /// ����AppSettings
        /// </summary>
        /// <param name="name">��</param>
        /// <param name="value">ֵ</param>
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
