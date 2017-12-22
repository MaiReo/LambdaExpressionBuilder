using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdaExpressionBuilder
{
    public abstract class ExpressionBuilder : IExpressionBuilder
    {
        internal static readonly Expression True;
        static ExpressionBuilder() => True = Expression.Constant( true, typeof( bool ) );
        protected ExpressionBuilder() => Expression = True;

        protected ExpressionBuilder( Expression expression ) => this.Expression = expression;

        public abstract Expression Expression { get; protected set; }


        public static IExpressionBuilder<T> BuildFor<T>() where T : class => new ExpressionBuilder<T>();

        public override string ToString() => Expression.ToString();
    }
    internal class ExpressionBuilder<T> : ExpressionBuilder, IExpressionBuilder<T>, IExpressionBuilder where T : class
    {
        protected internal ExpressionBuilder() : base() => this.Parameters = Default;

        protected internal ExpressionBuilder( Expression expression ) : base( expression ) => this.Parameters = Default;
        protected internal ExpressionBuilder( Expression expression, IEnumerable<ParameterExpression> parameters ) : this( expression ) => this.Parameters = parameters ?? Default;
        static ExpressionBuilder() => Default = Enumerable.Repeat( Expression.Parameter( typeof( T ), "src" ), 1 );
        public virtual IEnumerable<ParameterExpression> Parameters { get; protected set; }
        public override Expression Expression { get; protected set; }

        protected static readonly IEnumerable<ParameterExpression> Default;
    }


}
