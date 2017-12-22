using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdaExpressionBuilder
{
    public class ParameterReplacerVisitor : ExpressionVisitor
    {
        private readonly IEnumerable<ParameterExpression> _parameters;
        public ParameterReplacerVisitor(IEnumerable<ParameterExpression> parameters) => _parameters = parameters;
        protected override Expression VisitParameter(ParameterExpression node) => _parameters.FirstOrDefault(p => p.Type == node.Type) ?? base.VisitParameter(node);
    }
}
