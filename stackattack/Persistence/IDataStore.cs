using System.Collections.Generic;
using System.Data.Common;

namespace stackattack.Persistence
{
    public interface IDataStore<TItem>
    {
        TItem Save(DbConnection con, TItem item);
        TItem Get(DbConnection con, int id);
        IEnumerable<TItem> GetAll(DbConnection dbcon);
    }
}