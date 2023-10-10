using System;
using System.Collections.Generic;
using System.Text;

namespace infrastructure.storage.NoSql
{
    public abstract class DocumentBase
    {
        public string Id { get; set; }
        public DocumentBase Child { get; set; }
    }
}
