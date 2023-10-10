using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.storage.NoSql
{
    public interface IRepository
    {
        Task<T> AddEntity<T>(DocumentBase entity) where T : DocumentBase;
        Task AddEntityBatch<T>(List<T> entities) where T : DocumentBase;
        Task<T> GetEntity<T>(string id) where T : DocumentBase;
        Task<T> GetEntity<T>(DocumentBase request) where T : DocumentBase;
        Task DeleteEntity<T>(string id) where T : DocumentBase;
        Task DeleteEntity<T>(DocumentBase request) where T : DocumentBase;
        Task DeleteBatch<T>(List<string> ids) where T : DocumentBase;
        Task<T> UpdateEntity<T>(DocumentBase entity) where T : DocumentBase;
        Task UpdateEntityBatch<T>(List<T> entities) where T : DocumentBase;
        Task<T> RemoveArrayItems<T>(string documentId, string arrayPropertyName, params string[] items) where T : DocumentBase;
        Task<T> AddArrayItems<T>(string documentId, string arrayPropertyName, params string[] items) where T : DocumentBase;
        Task<List<T>> GetEntityByFieldEquality<T>(string fieldName, object value) where T : DocumentBase;
        Task DeleteEntitiesByFieldEquality<T>(string fieldName, object value) where T : DocumentBase;
    }
}
