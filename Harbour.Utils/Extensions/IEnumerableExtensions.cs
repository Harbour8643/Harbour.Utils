using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
namespace Harbour.Utils
{
    /// <summary>
    /// IEnumerable扩展函数
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// ForEach
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var element in source)
            {
                action(element);
                yield return element;
            }
        }
        /// <summary>
        /// Distinct
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="comparer">默认比较器</param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector, IEqualityComparer<V> comparer = null)
        {
            comparer = comparer == null ? EqualityComparer<V>.Default : comparer;
            return source.Distinct(ComparisonHelper<T>.CreateComparer(keySelector, comparer));
        }

        /// <summary>
        /// ForEach 用法:XX.ForEach((e,i)=>{  })
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static void TryForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            if (action != null)
            {
                int i = 0;
                foreach (var item in source)
                {
                    action(item, i);
                    ++i;
                }
            }
        }
        /// <summary>
        /// ForEach 用法:XX.ForEach({  })
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static void TryForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action != null)
            {
                foreach (var item in source)
                {
                    action(item);
                }
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sortField"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> list, string sortField, OrderByType orderByType)
        {
            PropertyInfo prop = typeof(T).GetProperty(sortField);
            if (prop == null)
            {
                throw new Exception("No property '" + sortField + "' in + " + typeof(T).Name + "'");
            }
            if (orderByType == OrderByType.DESC)
                return list.OrderByDescending(x => prop.GetValue(x, null));
            else
                return list.OrderBy(x => prop.GetValue(x, null));
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sortField"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> ThenBy<T>(this IOrderedEnumerable<T> list, string sortField, OrderByType orderByType)
        {
            PropertyInfo prop = typeof(T).GetProperty(sortField);

            if (orderByType == OrderByType.DESC)
                return list.OrderByDescending(x => prop.GetValue(x, null));

            if (orderByType == OrderByType.DESC)
                return list.ThenByDescending(x => prop.GetValue(x, null));
            else
                return list.ThenBy(x => prop.GetValue(x, null));

        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sortField"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> OrderByDataRow<T>(this IEnumerable<T> list, string sortField, OrderByType orderByType) where T : DataRow
        {
            PropertyInfo prop = typeof(T).GetProperty(sortField);
            if (orderByType == OrderByType.DESC)
                return list.OrderByDescending(it => it[sortField]);
            else
                return list.OrderBy(it => it[sortField]);

        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sortField"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> ThenByDataRow<T>(this IOrderedEnumerable<T> list, string sortField, OrderByType orderByType) where T : DataRow
        {
            PropertyInfo prop = typeof(T).GetProperty(sortField);
            if (orderByType == OrderByType.DESC)
                return list.ThenByDescending(it => it[sortField]);
            else
                return list.ThenBy(it => it[sortField]);

        }
        /// <summary>
        /// 排序类型
        /// </summary>
        public enum OrderByType
        {
            /// <summary>
            /// 顺序
            /// </summary>
            ASC = 1,
            /// <summary>
            /// 倒序
            /// </summary>
            DESC = 2
        }
    }
}
