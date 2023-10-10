using System;
using System.Collections.Generic;
using System.Text;

namespace infrastructure.storage.NoSql
{
    public abstract class EntityGroup : DocumentBase
    {
        //public string Id { get; set; }
        public EntityGroup Child { get; set; }
    }
}
