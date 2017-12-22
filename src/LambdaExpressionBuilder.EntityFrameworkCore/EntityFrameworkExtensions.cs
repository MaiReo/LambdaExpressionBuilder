using LambdaExpressionBuilder;
using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace System.Linq
{
    public static class EntityFrameworkExtensions
    {
        public static IQueryable<TSource> Build<TDbContext,TSource>(this TDbContext dbContext, IExpressionBuilder<TSource> builder) where TDbContext : DbContext where TSource : class
        {
            var dbContextExpression = Expression.Constant(dbContext,typeof(TDbContext));

            var sources = builder.GetParameterTypes()
            .Skip(1)
            .Where(t=>typeof(IQueryable).IsAssignableFrom(t))
            .Select(t=>t.GetGenericArguments().First())
            .Select(t=>Expression.Call(dbContextExpression,nameof(dbContext.Set),new[]{t},new Expression[0]));
            var expr = builder.BuildExpression(sources);
            return dbContext.Set<TSource>().Where(expr);
        }
    }
}