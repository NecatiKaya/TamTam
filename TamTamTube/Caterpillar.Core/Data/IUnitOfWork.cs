using System.Data;

namespace Caterpillar.Core.Data
{
    public interface IUnitOfWork
    {
        IDbCommand CreateCommand();

        IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);

        void SaveChanges();
    }
}
