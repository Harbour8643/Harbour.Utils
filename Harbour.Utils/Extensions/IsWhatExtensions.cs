using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Harbour.Utils
{

    /// <summary>
    /// IsWhat 扩展
    /// </summary>
    public static class IsWhatExtenions
    {

        /// <summary>
        /// 有值?
        /// </summary>
        /// <returns></returns>
        public static bool IsValuable(this object thisValue)
        {
            if (thisValue == null) return false;
            return thisValue.ToString() != "";
        }
        ///// <summary>
        ///// 有值?
        ///// </summary>
        ///// <returns></returns>
        //public static bool IsValuable(this IEnumerable<object> thisValue)
        //{
        //    if (thisValue == null || thisValue.Count() == 0) return false;
        //    return true;
        //}

        /// <summary>
        /// 有值?
        /// </summary>
        /// <returns></returns>
        public static bool IsValuable<T>(this IEnumerable<T> thisValue)
        {
            if (thisValue == null || thisValue.Count() == 0) return false;
            return true;
        }

        /// <summary>
        ///是适合正则匹配?
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static bool IsMatch(this object thisValue, string pattern)
        {
            if (thisValue == null) return false;
            Regex reg = new Regex(pattern);
            return reg.IsMatch(thisValue.ToString());
        }
    }
}
