using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Harbour.Utils
{
    /// <summary>
    /// IQueryable 扩展
    /// </summary>
    public static class IQueryableExtensions
    {
        #region Private expression tree helpers
        private static LambdaExpression GenerateSelector<TEntity>(String propertyName, out Type resultType) where TEntity : class
        {
            PropertyInfo property;
            Expression propertyAccess;
            var parameter = Expression.Parameter(typeof(TEntity), "Entity");

            if (propertyName.Contains('.'))
            {
                String[] childProperties = propertyName.Split('.');
                property = typeof(TEntity).GetProperty(childProperties[0]);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
                for (int i = 1; i < childProperties.Length; i++)
                {
                    property = property.PropertyType.GetProperty(childProperties[i]);
                    propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                }
            }
            else
            {
                property = typeof(TEntity).GetProperty(propertyName);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
            }

            resultType = property.PropertyType;

            return Expression.Lambda(propertyAccess, parameter);
        }
        private static MethodCallExpression GenerateMethodCall<TEntity>(IQueryable<TEntity> source, string methodName, String fieldName) where TEntity : class
        {
            Type type = typeof(TEntity);
            Type selectorResultType;
            LambdaExpression selector = GenerateSelector<TEntity>(fieldName, out selectorResultType);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName,
            new Type[] { type, selectorResultType },
            source.Expression, Expression.Quote(selector));
            return resultExp;
        }
        #endregion

        /// <summary>
        /// 顺序
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="fieldName">排序字段</param>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string fieldName) where TEntity : class
        {
            MethodCallExpression resultExp = GenerateMethodCall<TEntity>(source, "OrderBy", fieldName);
            return source.Provider.CreateQuery<TEntity>(resultExp) as IOrderedQueryable<TEntity>;
        }

        /// <summary>
        /// 降序
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="fieldName">排序字段</param>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> OrderByDescending<TEntity>(this IQueryable<TEntity> source, string fieldName) where TEntity : class
        {
            MethodCallExpression resultExp = GenerateMethodCall<TEntity>(source, "OrderByDescending", fieldName);
            return source.Provider.CreateQuery<TEntity>(resultExp) as IOrderedQueryable<TEntity>;
        }

        /// <summary>
        /// 顺序
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="fieldName">排序字段</param>
        /// <param name="orderByType">排序类型</param>
        /// <param name="defaultCol">默认排序字段</param>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string fieldName, OrderByType orderByType, string defaultCol) where TEntity : class
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                fieldName = defaultCol;
            }
            MethodCallExpression resultExp = GenerateMethodCall<TEntity>(source, orderByType.ToString(), fieldName);
            return source.Provider.CreateQuery<TEntity>(resultExp) as IOrderedQueryable<TEntity>;
        }
        /// <summary>
        /// ThenBy
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="fieldName">排序字段</param>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> ThenBy<TEntity>(this IOrderedQueryable<TEntity> source, string fieldName) where TEntity : class
        {
            MethodCallExpression resultExp = GenerateMethodCall<TEntity>(source, "ThenBy", fieldName);
            return source.Provider.CreateQuery<TEntity>(resultExp) as IOrderedQueryable<TEntity>;
        }

        /// <summary>
        /// ThenByDescending
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="fieldName">排序字段</param>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> ThenByDescending<TEntity>(this IOrderedQueryable<TEntity> source, string fieldName) where TEntity : class
        {
            MethodCallExpression resultExp = GenerateMethodCall<TEntity>(source, "ThenByDescending", fieldName);
            return source.Provider.CreateQuery<TEntity>(resultExp) as IOrderedQueryable<TEntity>;
        }

        /// <summary>
        /// OrderUsingSortExpression
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="sortExpression">排序表达式 eg:"ID ASC,Name DESC,Many ASC"</param>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> OrderUsingSortExpression<TEntity>(this IQueryable<TEntity> source, string sortExpression) where TEntity : class
        {
            String[] orderFields = sortExpression.Split(',');
            IOrderedQueryable<TEntity> result = null;
            for (int currentFieldIndex = 0; currentFieldIndex < orderFields.Length; currentFieldIndex++)
            {
                String[] expressionPart = orderFields[currentFieldIndex].Trim().Split(' ');
                String sortField = expressionPart[0];
                Boolean sortDescending = (expressionPart.Length == 2) && (expressionPart[1].Equals("DESC", StringComparison.OrdinalIgnoreCase));
                if (sortDescending)
                {
                    result = currentFieldIndex == 0 ? source.OrderByDescending(sortField) : result.ThenByDescending(sortField);
                }
                else
                {
                    result = currentFieldIndex == 0 ? source.OrderBy(sortField) : result.ThenBy(sortField);
                }
            }
            return result;
        }

        /// <summary>
        /// 排序类型
        /// </summary>
        public enum OrderByType
        {
            /// <summary>
            /// 顺序
            /// </summary>
            OrderBy = 1,
            /// <summary>
            /// 降序
            /// </summary>
            OrderByDescending = 2
        }
    }
}
