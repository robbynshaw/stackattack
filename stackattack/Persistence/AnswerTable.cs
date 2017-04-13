using stackattack.Answers;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Web;

namespace stackattack.Persistence
{
    internal class AnswerTable : SQLiteTable<Answer>
    {
        protected override string TableName { get { return "answers"; } }

        protected override string GetCreateSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"create table {this.TableName} (");
            sb.AppendLine("ID integer not null primary key,");
            sb.AppendLine("Title varchar(MAX) not null,");
            sb.AppendLine("Body varchar(MAX) not null,");
            sb.AppendLine("Guesses int");
            sb.AppendLine(")");
            return sb.ToString();
        }

        protected override string GetInsertSql(Answer item)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"insert into {this.TableName} (");
            sb.AppendLine("HighScore");
            sb.AppendLine(") values (");
            sb.AppendLine(item.Body);
            sb.AppendLine(");");
            sb.AppendLine("sqlite3_last_insert_rowid()");
            return sb.ToString();
        }

        protected override string GetUpdateSql(Answer item)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"update {this.TableName} set");
            sb.AppendLine($"HighScore = {item.ID}");
            sb.AppendLine($"where ID = {item.ID}");
            return sb.ToString();
        }

    }
}