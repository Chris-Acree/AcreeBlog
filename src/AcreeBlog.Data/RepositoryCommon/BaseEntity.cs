using System;
using System.Collections.Generic;
using System.Text;

namespace AcreeBlog.Data.RepositoryCommon
{
    public abstract class BaseEntity
    {
        public Int64 Id { get; protected set; }
    }
}
