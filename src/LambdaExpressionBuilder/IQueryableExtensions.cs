using LambdaExpressionBuilder;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace System.Linq
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> source, IExpressionBuilder<T> builder, params IQueryable[] sources) where T : class
        {
            return source.Where(builder.BuildExpression(sources));
        }
    }
}