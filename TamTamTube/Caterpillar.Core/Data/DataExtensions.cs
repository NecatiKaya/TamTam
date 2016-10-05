using System;
using System.Transactions;

namespace Caterpillar.Core.Data
{
    public static class DataExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        /// <param name="transactionTimeout">provide null value for 15 second to abort transaction</param>
        public static void RunInTransaction(this object obj, Action action, TimeSpan? transactionTimeout = null, TransactionScopeOption option = TransactionScopeOption.RequiresNew)
        {
            if (action != null)
            {
                TimeSpan timeoutForTransaction = transactionTimeout ?? TimeSpan.FromSeconds(15);
                using (TransactionScope scope = new TransactionScope(option, timeoutForTransaction))
                {
                    action();
                    scope.Complete();
                }                
                //scope is rollbacked
            }
        }
    }
}
