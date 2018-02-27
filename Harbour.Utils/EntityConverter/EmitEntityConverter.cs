using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;

namespace Harbour.Utils
{
    /// <summary>
    /// 实体转换类
    /// </summary>
    public class EmitEntityConverter
    {
        /// <summary>
        /// 将DataRow转为T
        /// </summary>
        /// <typeparam name="T">实体类（必须有默认构造参数）</typeparam>
        /// <param name="dr">DataRow</param>
        /// <returns></returns>
        public static T ToEntity<T>(DataRow dr) where T : new()
        {
            if (dr == null)
                return default(T);

            T t = new T();
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                if (dr.Table.Columns.Contains(prop.Name))
                {
                    if (dr[prop.Name] != DBNull.Value)
                        EmitSetter<T>(prop.Name)(t, dr[prop.Name]);
                }
            }
            return t;
        }
        /// <summary>
        /// 将IDataReader转换为实体
        /// </summary>
        /// <typeparam name="T">实体类（必须有默认构造参数）</typeparam>
        /// <param name="dr">IDataReader</param>
        /// <returns></returns>
        public static T ToEntity<T>(IDataReader dr) where T : new()
        {
            T t = default(T);
            if (dr.Read())
            {
                t = new T();
                foreach (PropertyInfo prop in typeof(T).GetProperties())
                {
                    if (dr[prop.Name] != DBNull.Value)
                        EmitSetter<T>(prop.Name)(t, dr[prop.Name]);
                }
            }
            return t;
        }

        /// <summary>
        /// 将DataTable转为List
        /// </summary>
        /// <typeparam name="T">实体类（必须有默认构造参数）</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<T> ToList<T>(DataTable dt) where T : new()
        {
            List<T> list = new List<T>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return list;
            }

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                foreach (PropertyInfo prop in typeof(T).GetProperties())
                {
                    if (dr.Table.Columns.Contains(prop.Name))
                    {
                        if (dr[prop.Name] != DBNull.Value)
                            EmitSetter<T>(prop.Name)(t, dr[prop.Name]);
                    }
                }
                list.Add(t);
            }

            return list;
        }
        /// <summary>
        /// 将IDataReader转为实体
        /// </summary>
        /// <typeparam name="T">实体类（必须有默认构造参数）</typeparam>
        /// <param name="dr">IDataReader</param>
        /// <returns></returns>
        public static List<T> ToList<T>(IDataReader dr) where T : new()
        {
            List<T> list = new List<T>();
            while (dr.Read())
            {
                T t = new T();
                foreach (PropertyInfo prop in typeof(T).GetProperties())
                {
                    if (dr[prop.Name] != DBNull.Value)
                        EmitSetter<T>(prop.Name)(t, dr[prop.Name]);
                }
                list.Add(t);
            }
            return list;
        }

        /// <summary>
        /// 通过Emit向T对象propertyName属性赋值
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="propertyName">对象属性名</param>
        /// <returns></returns>
        public static Action<T, object> EmitSetter<T>(string propertyName)
        {
            var type = typeof(T);
            var dynamicMethod = new DynamicMethod("EmitCallable", null, new[] { type, typeof(object) }, type.Module);
            var iLGenerator = dynamicMethod.GetILGenerator();

            var callMethod = type.GetMethod("set_" + propertyName, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public);
            var parameterInfo = callMethod.GetParameters()[0];
            var local = iLGenerator.DeclareLocal(parameterInfo.ParameterType, true);

            iLGenerator.Emit(OpCodes.Ldarg_1);
            if (parameterInfo.ParameterType.IsValueType)
            {
                // 如果是值类型，拆箱
                iLGenerator.Emit(OpCodes.Unbox_Any, parameterInfo.ParameterType);
            }
            else
            {
                // 如果是引用类型，转换
                iLGenerator.Emit(OpCodes.Castclass, parameterInfo.ParameterType);
            }

            iLGenerator.Emit(OpCodes.Stloc, local);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldloc, local);

            iLGenerator.EmitCall(OpCodes.Callvirt, callMethod, null);
            iLGenerator.Emit(OpCodes.Ret);

            return dynamicMethod.CreateDelegate(typeof(Action<T, object>)) as Action<T, object>;
        }
    }
}
