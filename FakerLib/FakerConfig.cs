using GeneratorPluginSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class FakerConfig
    {
        public Dictionary<Type, LambdaExpression> expressions = new Dictionary<Type, LambdaExpression>();
        public void Add<ClName, FType, Generat>(Expression<Func<ClName, FType>> expression)
        {
            ParameterExpression param = (ParameterExpression)expression.Parameters[0];
            MemberExpression mExpr = (MemberExpression)expression.Body;

            IGenerator generator = (IGenerator)Activator.CreateInstance(typeof(Generat));

            ConstantExpression constGen = Expression.Constant(generator);
            ParameterExpression fakerParam = Expression.Parameter(typeof(IFaker), "faker");
            MethodCallExpression callExpr = Expression.Call(constGen, typeof(Generat).GetMethod("Generate"), fakerParam);

            
            BinaryExpression binaryExpression = Expression.Assign(mExpr, Expression.Convert(callExpr, typeof(FType)));

            Expression<Action<ClName, IFaker>> resultExpr = Expression.Lambda<Action<ClName, IFaker>>(binaryExpression,
                new ParameterExpression[] { param, fakerParam});

            expressions.Add(typeof(ClName), resultExpr);
        }
    }
}
