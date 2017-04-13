using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace stackattack.Persistence
{
    internal abstract class SQLiteTable<TItem> where TItem : IDbReadable, new()
    {
        protected abstract string TableName { get; }

        protected abstract string GetCreateSql();
        protected abstract string GetInsertSql(TItem item);
        protected abstract string GetUpdateSql(TItem item);

        protected void ExecuteNonQuery(SQLiteConnection con, string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        protected IEnumerable<T> ExecuteReader<T>(SQLiteConnection con, string sql)
             where T : IDbReadable, new()
        {
            List<T> items = new List<T>();

            SQLiteCommand command = new SQLiteCommand(sql, con);
            using (SQLiteDataReader rdr = command.ExecuteReader())
            {
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        T item = new T();
                        item.Read(rdr);
                    }
                }
            }

            return items;
        }

        public virtual void Create(SQLiteConnection con)
        {
            string sql = GetCreateSql();
            ExecuteNonQuery(con, sql);
        }

        public virtual TItem Save(SQLiteConnection con, TItem item)
        {
            if (item.ID == int.MinValue)
            {
                string sql = GetInsertSql(item);
                SqLiteInt id = ExecuteReader<SqLiteInt>(con, sql).FirstOrDefault();
                item.ID = id.Value;
            }
            else
            {
                string sql = GetUpdateSql(item);
                ExecuteNonQuery(con, sql);
            }

            return item;
        }

        public virtual TItem Get(SQLiteConnection con, int id)
        {
            string sql = string.Format(
@"select from {0}
where ID = {1}", this.TableName, id);

            return ExecuteReader<TItem>(con, sql).FirstOrDefault();
        }
    }
}