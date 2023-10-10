using Google.Cloud.Firestore;
using infrastructure.storage.NoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.storage.firestore.NoSql
{
    public class FirestoreRepository : IRepository
    {
        private FirestoreConfig _config;
        private Dictionary<Type, string> _entityCollectionNames;
        private Dictionary<Type, string> _childEntityNames;
        protected FirestoreDb _database;

        public FirestoreRepository(FirestoreConfig config)
        {
            _config = config ?? throw new InvalidOperationException("Cannot initialize FirestoreRepository without an instance of FirestoreConfig. Please register an instance with the ServiceResolver Container")
;
            _entityCollectionNames = new Dictionary<Type, string>();
            _childEntityNames = new Dictionary<Type, string>();
        }

        public async Task<T> AddEntity<T>(DocumentBase entity) where T : DocumentBase
        {
            await EnsureDatabaseInitialised();

            var (docRef, lastEntity) = GetEntityDocRef(entity, true);            
            var result = await docRef.SetAsync(lastEntity);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            T entitySnapshot = snapshot.ConvertTo<T>();
            entitySnapshot.Id = snapshot.Id;

            return entitySnapshot;
        }

        public async Task AddEntityBatch<T>(List<T> entities) where T : DocumentBase
        {
            await EnsureDatabaseInitialised();

            var name = GetCollectionName(typeof(T));
            var collection = _database.Collection(name);
            var batch = _database.StartBatch();

            foreach (var entity in entities)
            {
                var docRef = string.IsNullOrEmpty(entity.Id) ? collection.Document() : collection.Document(entity.Id);
                batch.Create(docRef, entity);
            }

            await batch.CommitAsync();
        }

        public async Task<T> RemoveArrayItems<T>(string documentId, string arrayPropertyName, params string[] items) where T : DocumentBase
        {
            await EnsureDatabaseInitialised();

            var name = GetCollectionName(typeof(T));
            var collection = _database.Collection(name);

            var docRef= collection.Document(documentId);
            var writeResult = await docRef.UpdateAsync(arrayPropertyName, FieldValue.ArrayRemove(items));

            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            T entitySnapshot = snapshot.ConvertTo<T>();
            entitySnapshot.Id = snapshot.Id;
            return entitySnapshot;
        }

        public async Task<T> AddArrayItems<T>(string documentId, string arrayPropertyName, params string[] items) where T : DocumentBase
        {
            await EnsureDatabaseInitialised();

            var name = GetCollectionName(typeof(T));
            var collection = _database.Collection(name);

            var docRef = collection.Document(documentId);
            var writeResult = await docRef.UpdateAsync(arrayPropertyName, FieldValue.ArrayUnion(items));

            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            T entitySnapshot = snapshot.ConvertTo<T>();
            entitySnapshot.Id = snapshot.Id;
            return entitySnapshot;
        }

        public async Task UpdateEntityBatch<T>(List<T> entities) where T : DocumentBase
        {
            await EnsureDatabaseInitialised();

            var name = GetCollectionName(typeof(T));
            var collection = _database.Collection(name);
            var batch = _database.StartBatch();

            foreach (var entity in entities)
            {
                var docRef = string.IsNullOrEmpty(entity.Id) ? collection.Document() : collection.Document(entity.Id);
                batch.Set(docRef, entity, SetOptions.MergeAll);
            }

            await batch.CommitAsync();
        }
        
        public async Task<T> UpdateEntity<T>(DocumentBase entity) where T : DocumentBase
        {
            await EnsureDatabaseInitialised();

            var (docRef, lastEntity) = GetEntityDocRef(entity);
            var result = await docRef.SetAsync(entity, SetOptions.MergeAll);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            T entitySnapshot = snapshot.ConvertTo<T>();
            entitySnapshot.Id = snapshot.Id;
            return entitySnapshot;
        }

        public async Task<T> GetEntity<T>(string id) where T : DocumentBase
        {
            await EnsureDatabaseInitialised();

            var entityCollectionName = GetCollectionName(typeof(T));
            var collection = _database.Collection(entityCollectionName);
            var docRef = collection.Document(id);            
            var doc = await docRef.GetSnapshotAsync();
            if (!doc.Exists)
                return null;

            var entity = doc.ConvertTo<T>();
            entity.Id = doc.Id;
            return entity;
        }

        public async Task<T> GetEntity<T>(DocumentBase request) where T : DocumentBase
        {
            await EnsureDatabaseInitialised();

            var (docRef, lastEntity) = GetEntityDocRef(request);
            var doc = await docRef.GetSnapshotAsync();
            if (!doc.Exists)
                return null;

            var entity = doc.ConvertTo<T>();
            entity.Id = doc.Id;
            return entity;
        }

        public async Task<List<T>> GetEntityByFieldEquality<T>(string fieldName, object value) where T : DocumentBase
        {
            await EnsureDatabaseInitialised();

            var entityCollectionName = GetCollectionName(typeof(T));
            var collection = _database.Collection(entityCollectionName);
            var query = collection.WhereEqualTo(fieldName, value);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Documents.Select(x => {
                var doc = x.ConvertTo<T>();
                doc.Id = x.Id;
                return doc;
                }).ToList();
            
        }

        public async Task DeleteEntity<T>(string id) where T : DocumentBase
        {
            await EnsureDatabaseInitialised();

            var entityCollectionName = GetCollectionName(typeof(T));
            var collection = _database.Collection(entityCollectionName);
            var snap = await collection.GetSnapshotAsync();
            var docRef = collection.Document(id);            
            var result = await docRef.DeleteAsync();            
        }

        public async Task DeleteBatch<T>(List<string> ids) where T : DocumentBase
        {
            await EnsureDatabaseInitialised();

            var entityCollectionName = GetCollectionName(typeof(T));
            var collection = _database.Collection(entityCollectionName);
            var batch = _database.StartBatch();

            foreach (var id in ids)
            {
                batch.Delete(collection.Document(id));
            }

            await batch.CommitAsync();
        }

        public async Task DeleteEntity<T>(DocumentBase request) where T : DocumentBase
        {
            await EnsureDatabaseInitialised();
            var (docRef, lastEntity) = GetEntityDocRef(request);
            var result = await docRef.DeleteAsync();
        }

        public async Task DeleteEntitiesByFieldEquality<T>(string fieldName, object value) where T : DocumentBase
        {
            await EnsureDatabaseInitialised();

            var entityCollectionName = GetCollectionName(typeof(T));
            var collection = _database.Collection(entityCollectionName);
            var query = collection.WhereEqualTo(fieldName, value);
            var snapshot = await query.GetSnapshotAsync();
            var batch = _database.StartBatch();
            foreach(var doc in snapshot.Documents)
            {
                batch.Delete(doc.Reference);
            }

            await batch.CommitAsync();
        }

        protected async Task EnsureDatabaseInitialised()
        {
            if(_database == null)
            {
                _database = await FirestoreDb.CreateAsync(_config.ProjectId);
            }
        }
                
        protected string GetCollectionName(Type entityType)
        {
            string name = null;            
            if (_entityCollectionNames.TryGetValue(entityType, out name))
            {
                return name;
            }

            name = _config.EntityCollectionNameBuilder(entityType);
            var collectionPath = $"{_config.ApplicationName}/{_config.DatabaseCollectionName}/{name}";
            _entityCollectionNames.Add(entityType, collectionPath);

            return collectionPath;
        }
                
        private string GetChildEntityName(Type entityType)
        {
            string name = null;
            if (_childEntityNames.TryGetValue(entityType, out name))
            {
                return name;
            }

            name = _config.EntityCollectionNameBuilder(entityType);
            _childEntityNames.Add(entityType, name);

            return name;
        }

        private (DocumentReference docRef, DocumentBase entity) GetEntityDocRef(DocumentBase doc, bool createEachDocument  = false)
        {
            DocumentBase entity = doc;
            DocumentBase lastEntity = null;
            var name = GetCollectionName(doc.GetType());
            var collection = _database.Collection(name);
            DocumentReference docRef = null;
            do
            {
                docRef = string.IsNullOrEmpty(entity.Id) ? collection.Document() : collection.Document(entity.Id);
                if (createEachDocument)
                    docRef.CreateAsync(entity);

                lastEntity = entity;
                entity = entity.Child;
                if (entity != null)
                {
                    name = GetChildEntityName(entity.GetType());
                    collection = docRef.Collection(name);
                }
            } while (entity != null);
            return (docRef, lastEntity);
        }

    }
}
