using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ZbClassLibrary
{
    /// <summary>
    /// EF扩展工具类
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, String propertyName) where T : class
        {
            return QueryableHelper<T>.OrderBy(query, propertyName, false);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyName"></param>
        /// <param name="desc">是否倒序(true 倒序，false 顺序)</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, String propertyName, Boolean desc) where T : class
        {
            return QueryableHelper<T>.OrderBy(query, propertyName, desc);
        }

        static class QueryableHelper<T> where T : class
        {
            private static IDictionary<String, LambdaExpression> cache = new Dictionary<string, LambdaExpression>();
            public static IQueryable<T> OrderBy(IQueryable<T> query, String propertyName, Boolean desc)
            {
                dynamic keySelector = GetLambdaExpression(propertyName);
                return desc ? Queryable.OrderByDescending(query, keySelector) : Queryable.OrderBy(query, keySelector);
            }
            private static LambdaExpression GetLambdaExpression(String propertyName)
            {
                if (cache.ContainsKey(propertyName))
                {
                    return cache[propertyName];
                }
                ParameterExpression param = Expression.Parameter(typeof(T));
                MemberExpression body = Expression.Property(param, propertyName);
                LambdaExpression keySelector = Expression.Lambda(body, param);
                cache[propertyName] = keySelector;
                return keySelector;
            }
        }

        /// <summary>
        /// 实现In查询
        /// 例子：string[] names = new string[] { "a", "b" }; 
        /// var users = te.User.Where(ContainsExpression<User, string>(a => a.User_name, names));   
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="valueSelector"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Expression<Func<TElement, Boolean>> ContainsExpression<TElement, TValue>(Expression<Func<TElement, TValue>> valueSelector, IEnumerable<TValue> values)
        {
            if (valueSelector == null)
            {
                throw new ArgumentNullException("valueSelector");
            }
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            ParameterExpression parameterExpression = valueSelector.Parameters.Single();
            if (!values.Any())
            {
                return e => false;
            }
            IEnumerable<Expression> equals = values.Select(value => (Expression)Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof(TValue))));
            Expression body = equals.Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal));
            return Expression.Lambda<Func<TElement, Boolean>>(body, parameterExpression);
        }

        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge) where T : class
        {

            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            // apply composition of lambda expression bodies to parameters from the first expression
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);

        }

        /// <summary>
        /// And关键字
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Expression<Func<T, Boolean>> And<T>(this Expression<Func<T, Boolean>> first, Expression<Func<T, Boolean>> second) where T : class
        {
            return first.Compose(second, Expression.And);
        }

        /// <summary>
        /// Or关键字
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Expression<Func<T, Boolean>> Or<T>(this Expression<Func<T, Boolean>> first, Expression<Func<T, Boolean>> second) where T : class
        {
            return first.Compose(second, Expression.Or);
        }
    }

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }
}
