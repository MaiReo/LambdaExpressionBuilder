using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdaExpressionBuilder
{
    public class LambdaParameterReplacerVisitor : ExpressionVisitor
    {

        private readonly IEnumerable<KeyValuePair<ParameterExpression, Expression>> _target;

        public LambdaParameterReplacerVisitor
                ( IEnumerable<KeyValuePair<ParameterExpression, Expression>> target ) => _target = target;
        internal virtual Expression<TDelegate> VisitAndConvert<TDelegate>( LambdaExpression root ) => (Expression<TDelegate>)VisitLambdaExpression( typeof( TDelegate ), root );

        /// <summary>
        /// Leave all parameters alone except the one we want to replace.
        /// </summary>
        /// <param name="delegateType"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual Expression VisitLambdaExpression( System.Type delegateType, LambdaExpression node ) =>
            Expression.Lambda( delegateType, Visit( node.Body ), node.Parameters
                                 .Where( p => !_target.Select( t => t.Key ).Contains( p ) ) );

        protected override Expression VisitLambda<T>( Expression<T> node ) => VisitLambdaExpression( typeof( T ), node );

        /// <summary>
        /// Replace the source with the target, visit other params as usual.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitParameter( ParameterExpression node )
        {
            var target = _target.FirstOrDefault( p => p.Key == node );
            return default( KeyValuePair<ParameterExpression, Expression> ).Equals( target )
                ? base.VisitParameter( node )
                : target.Value;
        }
    }

}
