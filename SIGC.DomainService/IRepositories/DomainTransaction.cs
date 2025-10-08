using System.Transactions;

namespace SIGC.DomainService.IRepositories
{
    public class DomainTransaction : IDomainTransaction
    {
        TransactionScope TransactionScope;

        public void BeginTransaction()
        {
            TransactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        }

        public void CommitTransaction()
        {
            TransactionScope.Complete();
            TransactionScope.Dispose();
        }
        public void RollbackTransaction()
        {
            Dispose();
        }
        public void Dispose()
        {
            TransactionScope?.Dispose();
        }
    }
}