using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace Harbour.Utils
{
    /// <summary>
    /// 类型转换扩展类
    /// </summary>
    public static class TypeParseExtensions
    {
        /// <summary>
        /// 强转成GUID 如果失败返回 GUID.EMPTY
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static Guid TryToGuid(this object thisValue)
        {
            Guid reval = Guid.Empty;
            if (thisValue != null && Guid.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        /// <summary>
        /// 强转成bool 如果失败返回 false
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool TryToBoolean(this object thisValue)
        {
            bool reval = false;
            if (thisValue != null && Boolean.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        /// <summary>
        /// 强转成int 如果失败返回 defaultValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="defaultValue">默认值:0</param>
        /// <returns></returns>
        public static int TryToInt(this object thisValue, int defaultValue = 0)
        {
            int reval = 0;
            if (thisValue != null && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return defaultValue;
        }
        /// <summary>
        /// 强转成double 如果失败返回 defaultValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="defaultValue">默认值:0</param>
        /// <returns></returns>
        public static double TryToDouble(this object thisValue, double defaultValue = 0)
        {
            double reval = 0;
            if (thisValue != null && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return defaultValue;
        }
        /// <summary>
        /// 强转成string 如果失败返回 str
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="defaultValue">默认值:""</param>
        /// <returns></returns>
        public static string TryToString(this object thisValue, string defaultValue = "")
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return defaultValue;
        }
        /// <summary>
        /// 强转成Decimal 如果失败返回 defaultValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="defaultValue">默认值:0</param>
        /// <returns></returns>
        public static Decimal TryToDecimal(this object thisValue, decimal defaultValue = 0)
        {
            Decimal reval = 0;
            if (thisValue != null && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return defaultValue;
        }
        /// <summary>
        /// 强转成DateTime 如果失败返回 DateTime.MinValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static DateTime TryToDate(this object thisValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        /// <summary>
        /// 强转成DateTime 如果失败返回 defaultValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="defaultValue">默认值:DateTime.MinValue</param>
        /// <returns></returns>
        public static DateTime TryToDate(this object thisValue, DateTime defaultValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将指定的字符串数组转换成整型数组
        /// </summary>
        /// <param name="sourceStr">字符串数组</param>
        /// <param name="defaultValue">默认值 0</param>
        /// <returns></returns>
        public static int[] TryToIntArray(this string[] sourceStr, int defaultValue = 0)
        {
            if (sourceStr == null || sourceStr.Length <= 0)
                return new int[0] { };
            int[] retInt = new Int32[sourceStr.Length];
            for (int i = 0; i < sourceStr.Length; i++)
            {
                retInt[i] = sourceStr[i].TryToInt(defaultValue);
            }
            return retInt;
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="splitStr">分隔字符串</param>
        /// <returns></returns>
        public static string[] TryToStringArray(this string sourceStr, string splitStr = ",")
        {
            if (string.IsNullOrEmpty(sourceStr) || string.IsNullOrEmpty(splitStr))
                return new string[0] { };

            if (sourceStr.IndexOf(splitStr) == -1)
                return new string[] { sourceStr };

            if (splitStr.Length == 1)
                return sourceStr.Split(splitStr[0]);
            else
                return Regex.Split(sourceStr, Regex.Escape(splitStr), RegexOptions.IgnoreCase);

        }
        /// <summary>
        /// 分割字符串并转化为int[]
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="splitStr">分割符 默认：','</param>
        /// <param name="defaultValue">int默认值</param>
        /// <returns></returns>
        public static int[] TryToIntArray(this string sourceStr, string splitStr = ",", int defaultValue = 0)
        {
            return sourceStr.TryToStringArray(splitStr).TryToIntArray(defaultValue);
        }

        /// <summary>
        /// 将Json序列化为实体,注意，集合默认值为null
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="json"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static TEntity TryToModel<TEntity>(this string json, TEntity defaultValue = default(TEntity))
        {
            if (string.IsNullOrEmpty(json) || "null".Equals(json))
                return defaultValue;
            return JsonConvert.DeserializeObject<TEntity>(json);
        }
        /// <summary>
        /// 将实体序列化为Json
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string TryToJson<TEntity>(this TEntity model)
        {
            return JsonConvert.SerializeObject(model);
        }

        /// <summary>
        /// 将集合类转换成DataTable
        /// </summary>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static DataTable TryToDataTable<T>(this List<T> list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = typeof(T).GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        if (obj != null && obj != DBNull.Value)
                            tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
        /// <summary>
        /// 将datatable转为list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> TryToList<T>(this DataTable dt)
        {
            var list = new List<T>();
            Type t = typeof(T);
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());

            foreach (DataRow item in dt.Rows)
            {
                T s = System.Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                    if (info != null)
                    {
                        if (!Convert.IsDBNull(item[i]) && item[i] != null)
                        {
                            info.SetValue(s, item[i], null);
                        }
                    }
                }
                list.Add(s);
            }
            return list;
        }

        /// <summary>
        /// 将流转成btye
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] TryToBytes(this Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        /// <summary>
        /// 将btye转成流
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Stream TryToStream(this byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// 将string转成枚举
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static TEnum TryToEnum<TEnum>(this string thisValue)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), thisValue);
        }
        /// <summary>
        /// 将int转成枚举
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static TEnum TryToEnum<TEnum>(this int thisValue)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), thisValue.ToString());
        }
    }
}
