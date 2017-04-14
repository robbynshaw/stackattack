using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Web;

namespace stackattack.Persistence
{
    internal abstract class SQLiteTable<TItem> : IDataStore<TItem> where TItem : IDbReadable, new()
    {
        protected abstract string TableName { get; }

        protected abstract string GetCreateSql();
        protected abstract string GetInsertSql(SQLiteCommand c, TItem item);
        protected abstract string GetUpdateSql(SQLiteCommand c, TItem item);

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
                        items.Add(item);
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

        public virtual TItem Save(DbConnection dbcon, TItem item)
        {
            SQLiteConnection con = dbcon as SQLiteConnection;
            SQLiteCommand cmd = new SQLiteCommand(con);

            if (item.ID == 0)
            {
                string sql = GetInsertSql(cmd, item);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                item.ID = con.LastInsertRowId;
            }
            else
            {
                string sql = GetUpdateSql(cmd, item);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }

            return item;
        }

        public virtual TItem Get(DbConnection dbcon, int id)
        {
            SQLiteConnection con = dbcon as SQLiteConnection;

            string sql = string.Format(
@"select *
from {0}
where ID = {1}", this.TableName, id);

            return ExecuteReader<TItem>(con, sql).FirstOrDefault();
        }

        public IEnumerable<TItem> GetAll(DbConnection dbcon)
        {
            SQLiteConnection con = dbcon as SQLiteConnection;

            string sql = string.Format(
@"select *
from {0}", this.TableName);

            return ExecuteReader<TItem>(con, sql);
        }
    }
}