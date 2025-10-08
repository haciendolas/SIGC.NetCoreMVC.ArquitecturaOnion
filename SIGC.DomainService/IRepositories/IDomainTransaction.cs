namespace SIGC.DomainService.IRepositories
{
    public interface IDomainTransaction : IDisposable
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
