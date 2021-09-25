using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aerospike.Client;

namespace AerospikeExpressions.ExpressionBuilders
{
    public static class ArithmeticOps
    {
        public static Expression BuildNegativeNetBalanceCheckerExpression()
        {
            var addCondition = Exp.Add(Exp.Bin("bMargin", Exp.Type.FLOAT), Exp.Bin("bCash", Exp.Type.FLOAT));

            var gtCondition = Exp.LT(addCondition, Exp.Val(0.0));

            var balanceCheckExpression = Exp.Build(gtCondition);

            return balanceCheckExpression;
        }

        public static Expression BuildPositiveNetBalanceCheckerExpression()
        {
            var addCondition = Exp.Add(Exp.Bin("bMargin", Exp.Type.FLOAT), Exp.Bin("bCash", Exp.Type.FLOAT));

            var gtCondition = Exp.GT(addCondition, Exp.Val(0.0));

            var balanceCheckExpression = Exp.Build(gtCondition);

            return balanceCheckExpression;
        }

        public static Expression BuildAccountIdIndexExpression()
        {
            var indexCondition = Exp.Mod(Exp.Bin("bAccountId", Exp.Type.INT), Exp.Val(10000));

            var valueCondition = Exp.Val(8910);

            var accountIdIndexExpression = Exp.Build(Exp.EQ(indexCondition, valueCondition));

            return accountIdIndexExpression;
        }
    }
}
