using Google.Cloud.Firestore;
using infrastructure.storage.NoSql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.storage.firestore.NoSql
{
    public class TransactionalFirestoreRepository : FirestoreRepository, IRepositoryWithTransactions
    {
        public TransactionalFirestoreRepository(FirestoreConfig config) : base(config)
        {
            
        }

        public async Task UseTransaction(Func<Transaction, FirestoreDb, Task> transactionAction)
        {
            await EnsureDatabaseInitialised();

            await _database.RunTransactionAsync(async (trans) => await transactionAction(trans, _database));
        }

        public async Task<TReturn> UseTransaction<TReturn>(Func<Transaction, FirestoreDb, Task<TReturn>> transactionAction)
        {
            await EnsureDatabaseInitialised();

            var result = await _database.RunTransactionAsync<TReturn>(async trans => await transactionAction(trans, _database));

            return result;
        }
        
        public async Task<T> GetEntity<T>(Transaction transaction, string entityId) where T : DocumentBase
        {
            var collectionName = GetCollectionName(typeof(T));
            var collection = _database.Collection(collectionName);
            var docRef = collection.Document(entityId);
            var snapShot = await transaction.GetSnapshotAsync(docRef);
            if (!snapShot.Exists)
                return null;

            var entity = snapShot.ConvertTo<T>();
            entity.Id = snapShot.Id;
            return entity;
        }

        public void UpdateEntity<T>(Transaction transaction, DocumentBase entity) where T : DocumentBase
        {
            var collectionName = GetCollectionName(typeof(T));
            var collection = _database.Collection(collectionName);
            var docRef = collection.Document(entity.Id);

            transaction.Set(docRef, entity);
        }
    }
}
