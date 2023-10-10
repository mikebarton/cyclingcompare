using infrastructure.messaging.messages;
using System;
using System.Collections.Generic;
using System.Text;
using Google.Cloud.Firestore;
using infrastructure.storage.NoSql;

namespace infrastructure.entities
{
    [FirestoreData]
    public class ProcessedMessage<T> : DocumentBase
    {        
        [FirestoreProperty]
        public DateTime LastProcessed { get; set; }
        [FirestoreProperty]
        public string Message { get; set; }
        [FirestoreProperty]
        public string ErrorMessage { get; set; }
        [FirestoreProperty]
        public bool Success { get; set; }
        [FirestoreProperty]
        public int Attempts { get; set; }
    }
}
