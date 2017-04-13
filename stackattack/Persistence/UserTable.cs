using stackattack.Users;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Web;

namespace stackattack.Persistence
{
    internal class UserTable : SQLiteTable<User>
    {
        private const int TestUserID = 1;

        protected override string TableName { get { return "users"; } }

        protected override string GetCreateSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"create table {this.TableName} (");
            sb.AppendLine("ID integer not null primary key,");
            sb.AppendLine("HighScore integer");
            sb.AppendLine(")");
            return sb.ToString();
        }

        protected override string GetInsertSql(User item)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"insert into {this.TableName} (");
            sb.AppendLine("HighScore");
            sb.AppendLine(") values (");
            sb.AppendLine(item.HighScore);
            sb.AppendLine(");");
            sb.AppendLine("sqlite3_last_insert_rowid()");
            return sb.ToString();
        }

        protected override string GetUpdateSql(User item)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"update {this.TableName} set");
            sb.AppendLine($"HighScore = {item.HighScore}");
            sb.AppendLine($"where ID = {item.ID}");
            return sb.ToString();
        }

        public override void Create(SQLiteConnection con)
        {
            base.Create(con);

            // Create user for table existence verification
            User user = new User();
            Save(con, user);
        }

        public bool Exists(SQLiteConnection con)
        {
            return Get(con, TestUserID) != null;
        }
    }
}