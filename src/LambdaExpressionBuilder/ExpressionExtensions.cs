using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LambdaExpressionBuilder
{
    public static class ExpressionExtensions
    {
        public static Expression Replace( this Expression expression, IEnumerable<ParameterExpression> parameterExpressions ) => expression == null ? expression : new ParameterReplacerVisitor( parameterExpressions ).Visit( expression );
        public static Expression Replace( this Expression expression, params ParameterExpression[] parameters ) => Replace( expression, parameterExpressions: parameters );
        public static Expression<Func<T, bool>> Replace<T>( this Expression expression, IEnumerable<KeyValuePair<ParameterExpression, Expression>> target, ParameterExpression firstParameter = null ) => new LambdaParameterReplacerVisitor( target ).VisitAndConvert<Func<T, bool>>( Expression.Lambda( expression, new[] { firstParameter ?? Expression.Parameter( typeof( T ) ) }.Concat( target.Select( x => x.Key ) ) ) );


    }

}
