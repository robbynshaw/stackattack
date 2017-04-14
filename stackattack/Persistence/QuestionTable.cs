using stackattack.Core;
using stackattack.Questions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Web;

namespace stackattack.Persistence
{
    internal class QuestionTable : SQLiteTable<Question>
    {
        protected override string TableName { get { return "questions"; } }

        private void AddCommandParams(SQLiteCommand com, Question item)
        {
            com.Parameters.AddWithValue("@LastGuess", item.LastGuess);
            com.Parameters.AddWithValue("@Guesses", item.Guesses);
            com.Parameters.AddWithValue("@IsAnswered", item.IsAnswered);
            com.Parameters.AddWithValue("@ViewCount", item.ViewCount);
            com.Parameters.AddWithValue("@AcceptedAnswerID", item.AcceptedAnswerID);
            com.Parameters.AddWithValue("@Score", item.Score);
            com.Parameters.AddWithValue("@LastActivityDate", item.LastActivityDate);
            com.Parameters.AddWithValue("@CreationDate", item.CreationDate);
            com.Parameters.AddWithValue("@LastEditDate", item.LastEditDate);
            com.Parameters.AddWithValue("@QuestionID", item.QuestionID);
            com.Parameters.AddWithValue("@Link", item.Link);
            com.Parameters.AddWithValue("@Title", item.Title);
            com.Parameters.AddWithValue("@Body", item.Body);
        }

        protected override string GetCreateSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"create table {this.TableName} (");
            sb.AppendLine("ID integer not null primary key,");
            sb.AppendLine("LastGuess integer,");
            sb.AppendLine("Guesses integer,");
            sb.AppendLine("IsAnswered integer,");
            sb.AppendLine("ViewCount integer,");
            sb.AppendLine("AcceptedAnswerID integer,");
            sb.AppendLine("Score integer,");
            sb.AppendLine("LastActivityDate integer,");
            sb.AppendLine("CreationDate integer,");
            sb.AppendLine("LastEditDate integer,");
            sb.AppendLine("QuestionID integer not null,");
            sb.AppendLine("Link text,");
            sb.AppendLine("Title text not null,");
            sb.AppendLine("Body text,");
            sb.Length = sb.Length - 3;
            sb.AppendLine(")");
            return sb.ToString();
        }

        protected override string GetInsertSql(SQLiteCommand com, Question item)
        {
            AddCommandParams(com, item);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"insert into {this.TableName} (");
            sb.AppendLine("LastGuess,");
            sb.AppendLine("Guesses,");
            sb.AppendLine("IsAnswered,");
            sb.AppendLine("ViewCount,");
            sb.AppendLine("AcceptedAnswerID,");
            sb.AppendLine("Score,");
            sb.AppendLine("LastActivityDate,");
            sb.AppendLine("CreationDate,");
            sb.AppendLine("LastEditDate,");
            sb.AppendLine("QuestionID,");
            sb.AppendLine("Link,");
            sb.AppendLine("Title,");
            sb.AppendLine("Body,");
            sb.Length = sb.Length - 3;
            sb.AppendLine(") values (");
            sb.AppendLine("@LastGuess,");
            sb.AppendLine("@Guesses,");
            sb.AppendLine("@IsAnswered,");
            sb.AppendLine("@ViewCount,");
            sb.AppendLine("@AcceptedAnswerID,");
            sb.AppendLine("@Score,");
            sb.AppendLine("@LastActivityDate,");
            sb.AppendLine("@CreationDate,");
            sb.AppendLine("@LastEditDate,");
            sb.AppendLine("@QuestionID,");
            sb.AppendLine("@Link,");
            sb.AppendLine("@Title,");
            sb.AppendLine("@Body,");
            sb.Length = sb.Length - 3;
            sb.AppendLine(")");
            return sb.ToString();
        }

        protected override string GetUpdateSql(SQLiteCommand com, Question item)
        {
            AddCommandParams(com, item);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"update {this.TableName} set");
            sb.AppendLine("LastGuess = @LastGuess");
            sb.AppendLine("Guesses = @Guesses");
            sb.AppendLine("IsAnswered = @IsAnswered");
            sb.AppendLine("ViewCount = @ViewCount");
            sb.AppendLine("AcceptedAnswerID = @AcceptedAnswerID");
            sb.AppendLine("Score = @Score");
            sb.AppendLine("LastActivityDate = @LastActivityDate");
            sb.AppendLine("CreationDate = @CreationDate");
            sb.AppendLine("LastEditDate = @LastEditDate");
            sb.AppendLine("QuestionID = @QuestionID");
            sb.AppendLine("Link = @Link");
            sb.AppendLine("Title = @Title");
            sb.AppendLine("Body = @Body");
            sb.Length = sb.Length - 3;
            sb.AppendLine($"where ID = {item.ID}");
            return sb.ToString();
        }
    }
}