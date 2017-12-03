using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Harbour.Utils
{
    /// <summary>
    /// IEqualityComparer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ComparisonHelper<T>
    {
        /// <summary>
        /// 获取：IEqualityComparer
        /// 使用：  ComparisonHelper.CreateComparer(p => p.XX)
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector)
        {
            return new CommonEqualityComparer<V>(keySelector);
        }
        /// <summary>
        /// 获取：IEqualityComparer
        /// 使用：  ComparisonHelper.CreateComparer(p => p.XX)
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            return new CommonEqualityComparer<V>(keySelector, comparer);
        }

        class CommonEqualityComparer<V> : IEqualityComparer<T>
        {
            private Func<T, V> keySelector;
            private IEqualityComparer<V> comparer;

            public CommonEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer)
            {
                this.keySelector = keySelector;
                this.comparer = comparer;
            }
            public CommonEqualityComparer(Func<T, V> keySelector)
                : this(keySelector, EqualityComparer<V>.Default)
            { }

            public bool Equals(T x, T y)
            {
                if (x == null || y == null) return false;
                return comparer.Equals(keySelector(x), keySelector(y));
            }
            public int GetHashCode(T obj)
            {
                if (obj == null) return 0;
                return comparer.GetHashCode(keySelector(obj));
            }
        }

    }
}
