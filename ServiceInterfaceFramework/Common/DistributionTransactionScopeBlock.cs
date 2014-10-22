using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace ServiceInterfaceFramework.Common
{
    /// <summary>
    /// 分布式事务块
    /// </summary>
    public class DistributionTransactionScopeBlock
    {
        /// <summary>
        /// 新事务
        /// </summary>
        /// <param name="action">行为</param>
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

        private static object lockObject = new object();

        /// <summary>
        /// 新事物，加锁
        /// </summary>
        /// <param name="action">行为</param>
        public static void NewWithLock(Action action)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                TryCatchBlock.TrycatchAndLog(() =>
                {
                    lock (lockObject)
                    {
                        action();
                        scope.Complete();
                    }
                });
            }
        }
    }
}
