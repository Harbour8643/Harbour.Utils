using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harbour.Utils;

namespace Harbour.UtilsTest
{
    public static class TypeParseExtTest
    {
        public static V dfs<T, V>(this T obj, V s)
        {
            if (obj == null)
                return default(V);
            else
                return s;
        }

        public static void TryToIntTest()
        {
            object d = null;
            var ds = d.TryToInt();
        }

        public static void TryToIntArrayTest()
        {
            string[] arr = null;
            var ret = arr.TryToIntArray();
        }
        public static void TryToIntArray()
        {
            User user = null;
            int[] dfs = user?.Name.TryToIntArray();

            string sss = user?.Name;
            int[] sdds = sss.TryToIntArray();
            return;

            string s = null;
            var ret = s.TryToIntArray();
        }

        public static void TryToStringArrayTest()
        {
            string s = null;
            var ret = s.TryToStringArray();
        }

        public static void TryToJsonTest()
        {
            User user = null;
            var ret = user.TryToJson();
        }
    }
}
