using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdaExpressionBuilder
{
    public interface IExpressionBuilder
    {
        Expression Expression { get; }
    }

    public interface IExpressionBuilder<in T> : IExpressionBuilder where T : class
    {
        IEnumerable<ParameterExpression> Parameters { get; }
    }

}
