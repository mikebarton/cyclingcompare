using Google.Cloud.Firestore;
using infrastructure.storage.NoSql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace infrastructure.storage.firestore.NoSql
{
    public interface IRepositoryWithTransactions : IRepository
    {
        Task UseTransaction(Func<Transaction, FirestoreDb, Task> transactionAction);
        Task<TReturn> UseTransaction<TReturn>(Func<Transaction, FirestoreDb, Task<TReturn>> transactionAction);
        Task<T> GetEntity<T>(Transaction transaction, string entityId) where T : DocumentBase;
        void UpdateEntity<T>(Transaction transaction, DocumentBase entity) where T : DocumentBase;
    }
}
