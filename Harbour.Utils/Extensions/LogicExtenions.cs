using System.Linq;

namespace Harbour.Utils
{
    /// <summary>
    /// 逻辑判断类
    /// </summary>
    public static class LogicExtenions
    {
        /// <summary>
        /// 根据表达式的值，来返回两部分中的其中一个。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisValue"></param>
        /// <param name="trueValue">值为true返回 trueValue</param>
        /// <param name="falseValue">值为false返回 falseValue</param>
        /// <returns></returns>
        public static T IIF<T>(this bool thisValue, T trueValue, T falseValue)
        {
            if (thisValue)
            {
                return trueValue;
            }
            else
            {
                return falseValue;
            }
        }
        /// <summary>
        /// 根据表达式的值,true返回trueValue,false返回string.Empty;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisValue"></param>
        /// <param name="trueValue">值为true返回 trueValue</param>
        /// <returns></returns>
        public static string IIF(this bool thisValue, string trueValue)
        {
            return thisValue.IIF(trueValue, string.Empty);
        }
        /// <summary>
        /// 根据表达式的值,true返回trueValue,false返回0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisValue"></param>
        /// <param name="trueValue">值为true返回 trueValue</param>
        /// <returns></returns>
        public static int IIF(this bool thisValue, int trueValue)
        {
            return thisValue.IIF(trueValue, 0);
        }
        /// <summary>
        /// 根据表达式的值，来返回两部分中的其中一个。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisValue"></param>
        /// <param name="trueValue">值为true返回 trueValue</param>
        /// <param name="falseValue">值为false返回 falseValue</param>
        /// <returns></returns>
        public static T IIF<T>(this bool? thisValue, T trueValue, T falseValue)
        {
            if (thisValue == true)
            {
                return trueValue;
            }
            else
            {
                return falseValue;
            }
        }

        /// <summary>
        /// 所有值为true，则返回true否则返回false
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="andValues"></param>
        /// <returns></returns>
        public static bool And(this bool thisValue, params bool[] andValues)
        {
            return thisValue && !andValues.Where(c => c == false).Any();
        }
        /// <summary>
        /// 只要有一个值为true,则返回true否则返回false
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="andValues"></param>
        /// <returns></returns>
        public static bool Or(this bool thisValue, params bool[] andValues)
        {
            return thisValue || andValues.Where(c => c == true).Any();
        }

    }
}
