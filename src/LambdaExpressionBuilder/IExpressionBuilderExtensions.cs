using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdaExpressionBuilder
{
    public static class IExpressionBuilderExtensions
    {
        #region BuildExpression
        private static Expression<Func<T, bool>> BuildExpressionTrue<T>( IExpressionBuilder<T> builder ) where T : class => Expression.Lambda<Func<T, bool>>( builder.Expression, Expression.Parameter( typeof( T ) ) );

        public static Expression<Func<T, bool>> BuildExpression<T>( this IExpressionBuilder<T> builder, IEnumerable<IQueryable> sources = null ) where T : class => BuildExpression( builder, sources?.Select( s => Expression.Constant( s, typeof( IQueryable<> ).MakeGenericType( s.GetType().GetGenericArguments() ) ) ) );

        public static Expression<Func<T, bool>> BuildExpression<T>( this IExpressionBuilder<T> builder, IEnumerable<Expression> sources = null ) where T : class
        {
            if (builder == null) throw new ArgumentNullException( nameof( builder ) );
            if (builder.Expression == ExpressionBuilder.True) return BuildExpressionTrue( builder );
            if (sources == null) throw new ArgumentNullException( nameof( sources ) );
            var parameterValues = sources.Select( s => Expression.Convert( s, typeof( IQueryable<> ).MakeGenericType( s.Type.GetGenericArguments() ) ) );
            if (parameterValues.Count() < builder.Parameters.Count() - 1) throw new ArgumentOutOfRangeException( nameof( parameterValues ), "The element count of parameterValues should be greater than or equals to the count of builder.Parameters - 1" );
            var firstParameter = builder.Parameters.FirstOrDefault() ?? Expression.Parameter( typeof( T ) );
            return builder.Expression.Replace( firstParameter ).Replace<T>(
            builder.Parameters.Skip( 1 ).Zip( parameterValues, ( k, v ) => new KeyValuePair<ParameterExpression, Expression>( k, v ) ),
            firstParameter );
        }

        #endregion BuildExpression

        #region Where
        private static IExpressionBuilder<T> WhereLambda<T>( IExpressionBuilder<T> builder, LambdaExpression expression ) where T : class
        {
            var oExpr = builder.Expression == ExpressionBuilder.True ? null : builder.Expression;
            var nExpr = expression.Body.Replace( builder.Parameters );
            var nBody = oExpr == null ? nExpr : Expression.AndAlso( oExpr, nExpr );
            var nParameters = new List<ParameterExpression>( builder.Parameters );
            foreach (var ep in expression.Parameters)
            {
                if (builder.Parameters.Any( p => p.Type == ep.Type )) continue;
                nParameters.Add( ep );
            }
            return new ExpressionBuilder<T>( nBody, nParameters );
        }
        public static IExpressionBuilder<T> Where<T>( this IExpressionBuilder<T> builder, Expression<Func<T, bool>> expression ) where T : class => WhereLambda( builder, expression );
        public static IExpressionBuilder<T1> Where<T1, T2>( this IExpressionBuilder<T1> builder, Expression<Func<T1, IQueryable<T2>, bool>> expression ) where T1 : class where T2 : class => WhereLambda( builder, expression );
        public static IExpressionBuilder<T1> Where<T1, T2, T3>( this IExpressionBuilder<T1> builder, Expression<Func<T1, IQueryable<T2>, IQueryable<T3>, bool>> expression ) where T1 : class where T2 : class where T3 : class => WhereLambda( builder, expression );
        public static IExpressionBuilder<T1> Where<T1, T2, T3, T4>( this IExpressionBuilder<T1> builder, Expression<Func<T1, IQueryable<T2>, IQueryable<T3>, IQueryable<T4>, bool>> expression ) where T1 : class where T2 : class where T3 : class where T4 : class => WhereLambda( builder, expression );
        public static IExpressionBuilder<T1> Where<T1, T2, T3, T4, T5>( this IExpressionBuilder<T1> builder, Expression<Func<T1, IQueryable<T2>, IQueryable<T3>, IQueryable<T4>, IQueryable<T5>, bool>> expression ) where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class => WhereLambda( builder, expression );
        public static IExpressionBuilder<T1> Where<T1, T2, T3, T4, T5, T6>( this IExpressionBuilder<T1> builder, Expression<Func<T1, IQueryable<T2>, IQueryable<T3>, IQueryable<T4>, IQueryable<T5>, IQueryable<T6>, bool>> expression ) where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class => WhereLambda( builder, expression );
        public static IExpressionBuilder<T1> Where<T1, T2, T3, T4, T5, T6, T7>( this IExpressionBuilder<T1> builder, Expression<Func<T1, IQueryable<T2>, IQueryable<T3>, IQueryable<T4>, IQueryable<T5>, IQueryable<T6>, IQueryable<T7>, bool>> expression ) where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class => WhereLambda( builder, expression );
        public static IExpressionBuilder<T1> Where<T1, T2, T3, T4, T5, T6, T7, T8>( this IExpressionBuilder<T1> builder, Expression<Func<T1, IQueryable<T2>, IQueryable<T3>, IQueryable<T4>, IQueryable<T5>, IQueryable<T6>, IQueryable<T7>, IQueryable<T8>, bool>> expression ) where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class => WhereLambda( builder, expression );
        public static IExpressionBuilder<T1> Where<T1, T2, T3, T4, T5, T6, T7, T8, T9>( this IExpressionBuilder<T1> builder, Expression<Func<T1, IQueryable<T2>, IQueryable<T3>, IQueryable<T4>, IQueryable<T5>, IQueryable<T6>, IQueryable<T7>, IQueryable<T8>, IQueryable<T9>, bool>> expression ) where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class => WhereLambda( builder, expression );
        public static IExpressionBuilder<T1> Where<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>( this IExpressionBuilder<T1> builder, Expression<Func<T1, IQueryable<T2>, IQueryable<T3>, IQueryable<T4>, IQueryable<T5>, IQueryable<T6>, IQueryable<T7>, IQueryable<T8>, IQueryable<T9>, IQueryable<T10>, bool>> expression ) where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class where T10 : class => WhereLambda( builder, expression );
        public static IExpressionBuilder<T1> Where<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>( this IExpressionBuilder<T1> builder, Expression<Func<T1, IQueryable<T2>, IQueryable<T3>, IQueryable<T4>, IQueryable<T5>, IQueryable<T6>, IQueryable<T7>, IQueryable<T8>, IQueryable<T9>, IQueryable<T10>, IQueryable<T11>, bool>> expression ) where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class where T10 : class where T11 : class => WhereLambda( builder, expression );
        public static IExpressionBuilder<T1> Where<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>( this IExpressionBuilder<T1> builder, Expression<Func<T1, IQueryable<T2>, IQueryable<T3>, IQueryable<T4>, IQueryable<T5>, IQueryable<T6>, IQueryable<T7>, IQueryable<T8>, IQueryable<T9>, IQueryable<T10>, IQueryable<T11>, IQueryable<T12>, bool>> expression ) where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class where T10 : class where T11 : class where T12 : class => WhereLambda( builder, expression );
        public static IExpressionBuilder<T1> Where<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>( this IExpressionBuilder<T1> builder, Expression<Func<T1, IQueryable<T2>, IQueryable<T3>, IQueryable<T4>, IQueryable<T5>, IQueryable<T6>, IQueryable<T7>, IQueryable<T8>, IQueryable<T9>, IQueryable<T10>, IQueryable<T11>, IQueryable<T12>, IQueryable<T13>, bool>> expression ) where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class where T10 : class where T11 : class where T12 : class where T13 : class => WhereLambda( builder, expression );
        public static IExpressionBuilder<T1> Where<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>( this IExpressionBuilder<T1> builder, Expression<Func<T1, IQueryable<T2>, IQueryable<T3>, IQueryable<T4>, IQueryable<T5>, IQueryable<T6>, IQueryable<T7>, IQueryable<T8>, IQueryable<T9>, IQueryable<T10>, IQueryable<T11>, IQueryable<T12>, IQueryable<T13>, IQueryable<T14>, bool>> expression ) where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class where T10 : class where T11 : class where T12 : class where T13 : class where T14 : class => WhereLambda( builder, expression );
        public static IExpressionBuilder<T1> Where<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>( this IExpressionBuilder<T1> builder, Expression<Func<T1, IQueryable<T2>, IQueryable<T3>, IQueryable<T4>, IQueryable<T5>, IQueryable<T6>, IQueryable<T7>, IQueryable<T8>, IQueryable<T9>, IQueryable<T10>, IQueryable<T11>, IQueryable<T12>, IQueryable<T13>, IQueryable<T14>, IQueryable<T15>, bool>> expression ) where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class where T10 : class where T11 : class where T12 : class where T13 : class where T14 : class where T15 : class => WhereLambda( builder, expression );
        public static IExpressionBuilder<T1> Where<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>( this IExpressionBuilder<T1> builder, Expression<Func<T1, IQueryable<T2>, IQueryable<T3>, IQueryable<T4>, IQueryable<T5>, IQueryable<T6>, IQueryable<T7>, IQueryable<T8>, IQueryable<T9>, IQueryable<T10>, IQueryable<T11>, IQueryable<T12>, IQueryable<T13>, IQueryable<T14>, IQueryable<T15>, IQueryable<T16>, bool>> expression ) where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class where T10 : class where T11 : class where T12 : class where T13 : class where T14 : class where T15 : class where T16 : class => WhereLambda( builder, expression );


        #endregion Where

        public static IEnumerable<Type> GetParameterTypes<T>( this IExpressionBuilder<T> builder ) where T : class => builder.Parameters.Select( p => p.Type );
    }
}