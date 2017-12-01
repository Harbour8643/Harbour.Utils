using System;
using System.Text.RegularExpressions;

namespace Harbour.Utils
{
    /// <summary>
    /// 日期类型帮助类
    /// </summary>
    public class DateTimeHelper
    {
        private readonly static int _defaultYear = 1999;//默认年
        private readonly static int _defaultMonth = 1;//默认月
        private readonly static int _defaultDay = 1;//默认天
        private readonly static int _defaultHour = 0;//默认小时
        private readonly static int _defaultMinute = 0;//默认分钟
        private readonly static int _defaultSecond = 0;//默认秒

        /// <summary>
        /// 将string类型转换成int类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        private static int StringToInt(string s, int defaultValue)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                int result;
                if (int.TryParse(s, out result))
                    return result;
            }

            return defaultValue;
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="splitStr">分隔字符串</param>
        /// <returns></returns>
        private static string[] SplitString(string sourceStr, string splitStr)
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
        /// 根据时间得到DateTime
        /// </summary>
        /// <param name="Time">时间，eg:8:30</param>
        /// <returns></returns>
        public static DateTime GetDateTime(string Time)
        {
            return GetDateTime(Time, ":");
        }

        /// <summary>
        /// 根据时间得到DateTime
        /// </summary>
        /// <param name="Time">时间，eg:8:30</param>
        /// <param name="splitStr">分割符，eg:“;”</param> 
        /// <returns></returns>
        public static DateTime GetDateTime(string Time, string splitStr)
        {
            string[] HourAndMinute = SplitString(Time, splitStr);
            if (HourAndMinute.Length < 2)
                return GetDateTime(_defaultSecond);
            int Hour = StringToInt(HourAndMinute[0], _defaultHour);
            int Minute = StringToInt(HourAndMinute[1], _defaultMinute);
            return GetDateTime(Hour, Minute, _defaultSecond);
        }

        /// <summary>
        /// 获得日期
        /// </summary>
        /// <param name="Second">秒</param>
        /// <returns></returns>
        public static DateTime GetDateTime(int Second)
        {
            return GetDateTime(_defaultHour, _defaultMinute, Second);
        }

        /// <summary>
        /// 获得日期
        /// </summary>
        /// <param name="Minute">分</param>
        /// <param name="Second">秒</param>
        /// <returns></returns>
        public static DateTime GetDateTime(int Minute, int Second)
        {
            return GetDateTime(_defaultHour, Minute, Second);
        }

        /// <summary>
        /// 获得日期
        /// </summary>
        /// <param name="Hour">时</param>
        /// <param name="Minute">分</param>
        /// <param name="Second">秒</param>
        /// <returns></returns>
        public static DateTime GetDateTime(int Hour, int Minute, int Second)
        {
            return new DateTime(_defaultYear, _defaultMonth, _defaultDay, Hour, Minute, Second);
        }

        /// <summary>
        /// 返回与CompareTime相比时间差最小的时间
        /// </summary>
        /// <param name="CompareTime">被比较</param>
        /// <param name="Time1">比较者</param>
        /// <param name="time2">比较者</param>
        /// <returns></returns>
        public static DateTime GetSmallTimeSpan(DateTime CompareTime, DateTime Time1, DateTime time2)
        {
            TimeSpan TimeSpan1 = Time1 - CompareTime;
            TimeSpan TimeSpan2 = time2 - CompareTime;
            return TimeSpan.Compare(TimeSpan1, TimeSpan2) < 0 ? Time1 : time2;
        }

        /// <summary>
        /// 得到时间的小时和分钟，eg:8:09
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetDecimalHourMinute(DateTime dateTime)
        {
            string Minute = null;
            string Hour = null;
            if (dateTime.Minute < 10)
                Minute = "0" + dateTime.Minute;
            else
                Minute = dateTime.Minute.ToString();

            if (dateTime.Hour < 10)
                Hour = "0" + dateTime.Hour;
            else
                Hour = dateTime.Hour.ToString();
            return Hour + ":" + Minute;
        }

        /// <summary>
        /// 得到随机日期
        /// </summary>
        /// <param name="StartTime">起始日期</param>
        /// <param name="EndTime">结束日期</param>
        /// <returns>间隔日期之间的 随机日期</returns>
        public static DateTime GetRandomTime(DateTime StartTime, DateTime EndTime)
        {
            Random random = new Random();
            DateTime minTime = new DateTime();
            DateTime maxTime = new DateTime();

            System.TimeSpan ts = new System.TimeSpan(StartTime.Ticks - EndTime.Ticks);

            // 获取两个时间相隔的秒数
            double dTotalSecontds = ts.TotalSeconds;
            int iTotalSecontds = 0;

            if (dTotalSecontds > System.Int32.MaxValue)
            {
                iTotalSecontds = System.Int32.MaxValue;
            }
            else if (dTotalSecontds < System.Int32.MinValue)
            {
                iTotalSecontds = System.Int32.MinValue;
            }
            else
            {
                iTotalSecontds = (int)dTotalSecontds;
            }


            if (iTotalSecontds > 0)
            {
                minTime = EndTime;
                maxTime = StartTime;
            }
            else if (iTotalSecontds < 0)
            {
                minTime = StartTime;
                maxTime = EndTime;
            }
            else
            {
                return StartTime;
            }

            int maxValue = iTotalSecontds;

            if (iTotalSecontds <= System.Int32.MinValue)
                maxValue = System.Int32.MinValue + 1;

            int i = random.Next(System.Math.Abs(maxValue));

            return minTime.AddSeconds(i);
        }

        /// <summary>
        /// 格式化日期时间
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="dateMode">显示模式</param>
        /// <returns>0-9种模式的日期</returns>
        public static string DateTimeFormat(DateTime dateTime, DateMode dateMode)
        {
            switch (dateMode)
            {
                case DateMode.yyyy_MM_dd:
                    return dateTime.ToString("yyyy-MM-dd");
                case DateMode.yyyy_MM_dd_HH_mm_ss:
                    return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                case DateMode.yyyyMMdd:
                    return dateTime.ToString("yyyy/MM/dd");
                case DateMode.yyyy_Year_MM_Month_dd_Day:
                    return dateTime.ToString("yyyy年MM月dd日");
                case DateMode.MM_dd:
                    return dateTime.ToString("MM-dd");
                case DateMode.MMdd:
                    return dateTime.ToString("MM/dd");
                case DateMode.MM_Month_dd_Day:
                    return dateTime.ToString("MM月dd日");
                case DateMode.yyyy_MM:
                    return dateTime.ToString("yyyy-MM");
                case DateMode.yyyyMM:
                    return dateTime.ToString("yyyy/MM");
                case DateMode.yyyy_Year_MM_Month:
                    return dateTime.ToString("yyyy年MM月");
                case DateMode.Default:
                    return dateTime.ToString();
                default: return dateTime.ToString();
            }
        }

        /// <summary>
        /// 判段两个时间是否是同一天
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool IsSameDay(DateTime? d1, DateTime? d2)
        {
            if (d1 == null)
            {
                d1 = DateTime.MinValue;
            }
            if (d2 == null)
            {
                d2 = DateTime.MinValue;
            }
            return
                IsSameDay(Convert.ToDateTime(d1), Convert.ToDateTime(d2));
        }
        /// <summary>
        /// 判段两个时间是否是同一天
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool IsSameDay(DateTime d1, DateTime d2)
        {
            return d1.ToString("yyyy-MM-dd") == d2.ToString("yyyy-MM-dd");
        }

        public static string DateStringFromNow(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 60)
            {
                return dt.ToShortDateString();
            }
            else
            {
                if (span.TotalDays > 30)
                {
                    return
                    "1个月前";
                }
                else
                {
                    if (span.TotalDays > 14)
                    {
                        return
                        "2周前";
                    }
                    else
                    {
                        if (span.TotalDays > 7)
                        {
                            return
                            "1周前";
                        }
                        else
                        {
                            if (span.TotalDays > 1)
                            {
                                return
                                string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
                            }
                            else
                            {
                                if (span.TotalHours > 1)
                                {
                                    return
                                    string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
                                }
                                else
                                {
                                    if (span.TotalMinutes > 1)
                                    {
                                        return
                                        string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
                                    }
                                    else
                                    {
                                        if (span.TotalSeconds >= 1)
                                        {
                                            return
                                            string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
                                        }
                                        else
                                        {
                                            return
                                            "1秒前";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 日期模式
    /// </summary>
    public enum DateMode
    {
        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        yyyy_MM_dd = 0,

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        yyyy_MM_dd_HH_mm_ss = 1,

        /// <summary>
        /// yyyy/MM/dd
        /// </summary>
        yyyyMMdd = 2,

        /// <summary>
        /// yyyy年MM月dd日
        /// </summary>
        yyyy_Year_MM_Month_dd_Day = 3,

        /// <summary>
        /// MM-dd
        /// </summary>
        MM_dd = 4,

        /// <summary>
        /// MM/dd
        /// </summary>
        MMdd = 5,

        /// <summary>
        /// MM月dd日
        /// </summary>
        MM_Month_dd_Day = 6,

        /// <summary>
        /// yyyy-MM
        /// </summary>
        yyyy_MM = 7,

        /// <summary>
        /// yyyy/MM
        /// </summary>
        yyyyMM = 8,

        /// <summary>
        /// yyyy年MM月
        /// </summary>
        yyyy_Year_MM_Month = 9,

        /// <summary>
        /// 默认
        /// </summary>
        Default = 10

    }

}
