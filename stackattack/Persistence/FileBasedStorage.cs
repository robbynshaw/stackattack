using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using stackattack.Users;
using System.Data.SQLite;

namespace stackattack.Persistence
{
    internal class FileBasedStorage : IStorageProvider
    {
        private const string dbname = "stackattack.sqlite";

        private UserTable UserTable;

        private void CreateDatabase()
        {
            SQLiteConnection.CreateFile(dbname);
        }

        private SQLiteConnection GetConnection()
        {
            SQLiteConnection con;

            try
            {
                con = new SQLiteConnection($"Data Source={dbname};Version=3;");
                con.Open();
            }
            catch
            {
                CreateDatabase();
                con = new SQLiteConnection($"Data Source={dbname};Version=3;");
                con.Open();
            }

            return con;
        }

        private bool TablesExist(SQLiteConnection con)
        {
            return this.UserTable.Exists(con);
        }

        private void CreateTables(SQLiteConnection con)
        {
            this.UserTable.Create(con);
        }

        public void Load()
        {
            using (SQLiteConnection con = GetConnection())
            {
                if (!TablesExist(con))
                {
                    CreateTables(con);
                }
            }
        }

        public IUser GetUser()
        {
            throw new NotImplementedException();
        }

        public void SaveUser()
        {
            throw new NotImplementedException();
        }

    }
}