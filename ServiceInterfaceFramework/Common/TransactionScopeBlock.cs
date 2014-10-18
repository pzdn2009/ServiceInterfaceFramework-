using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace ServiceInterfaceFramework.Common
{
    public class TransactionScopeBlock
    {
        public static void New(Action action)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                TryCatchBlock.TrycatchAndLog(() =>
                {
                    action();
                    scope.Complete();
                });
            }
        }
    }
}
