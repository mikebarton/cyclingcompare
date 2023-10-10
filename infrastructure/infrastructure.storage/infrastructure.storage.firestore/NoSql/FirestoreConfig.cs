using System;
using System.Collections.Generic;
using System.Text;

namespace infrastructure.storage.firestore.NoSql
{
    public class FirestoreConfig
    {
        public string ApplicationName { get; set; }
        public string ProjectId { get; set; }
        public string DatabaseCollectionName { get; set; }
        public Func<Type, string> EntityCollectionNameBuilder { get; set; }
    }
}
