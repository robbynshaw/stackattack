using stackattack.Answers;
using stackattack.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Web;

namespace stackattack.Persistence
{
    internal class AnswerTable : SQLiteTable<Answer>
    {
        protected override string TableName { get { return "answers"; } }

        private void AddCommandParams(SQLiteCommand com, Answer item)
        {
            com.Parameters.AddWithValue("@DBQuestionID", item.DBQuestionID);
            com.Parameters.AddWithValue("@GuessScore", item.GuessScore);
            com.Parameters.AddWithValue("@Guesses", item.Guesses);
            com.Parameters.AddWithValue("@IsAccepted", item.IsAccepted);
            com.Parameters.AddWithValue("@Score", item.Score);
            com.Parameters.AddWithValue("@LastActivityDate", item.LastActivityDate);
            com.Parameters.AddWithValue("@LastEditDate", item.LastEditDate);
            com.Parameters.AddWithValue("@CreationDate", item.CreationDate);
            com.Parameters.AddWithValue("@AnswerID", item.AnswerID);
            com.Parameters.AddWithValue("@QuestionID", item.QuestionID);
            com.Parameters.AddWithValue("@Body", item.Body);
        }

        protected override string GetCreateSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"create table {this.TableName} (");
            sb.AppendLine("ID integer not null primary key,");
            sb.AppendLine("DBQuestionID integer,");
            sb.AppendLine("GuessScore integer,");
            sb.AppendLine("Guesses integer,");
            sb.AppendLine("IsAccepted integer,");
            sb.AppendLine("Score integer,");
            sb.AppendLine("LastActivityDate integer,");
            sb.AppendLine("LastEditDate integer,");
            sb.AppendLine("CreationDate integer,");
            sb.AppendLine("AnswerID integer not null,");
            sb.AppendLine("QuestionID integer not null,");
            sb.AppendLine("Body text,");
            sb.Length = sb.Length - 3;
            sb.AppendLine(")");
            return sb.ToString();
        }

        protected override string GetInsertSql(SQLiteCommand com, Answer item)
        {
            AddCommandParams(com, item);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"insert into {this.TableName} (");
            sb.AppendLine("DBQuestionID,");
            sb.AppendLine("GuessScore,");
            sb.AppendLine("Guesses,");
            sb.AppendLine("IsAccepted,");
            sb.AppendLine("Score,");
            sb.AppendLine("LastActivityDate,");
            sb.AppendLine("LastEditDate,");
            sb.AppendLine("CreationDate,");
            sb.AppendLine("AnswerID,");
            sb.AppendLine("QuestionID,");
            sb.AppendLine("Body,");
            sb.Length = sb.Length - 3;
            sb.AppendLine(") values (");
            sb.AppendLine("@DBQuestionID,");
            sb.AppendLine("@GuessScore,");
            sb.AppendLine("@Guesses,");
            sb.AppendLine("@IsAccepted,");
            sb.AppendLine("@Score,");
            sb.AppendLine("@LastActivityDate,");
            sb.AppendLine("@LastEditDate,");
            sb.AppendLine("@CreationDate,");
            sb.AppendLine("@AnswerID,");
            sb.AppendLine("@QuestionID,");
            sb.AppendLine("@Body,");
            sb.Length = sb.Length - 3;
            sb.AppendLine(")");
            return sb.ToString();
        }

        protected override string GetUpdateSql(SQLiteCommand com, Answer item)
        {
            AddCommandParams(com, item);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"update {this.TableName} set");
            sb.AppendLine("DBQuestionID = @DBQuestionID,");
            sb.AppendLine("GuessScore = @GuessScore,");
            sb.AppendLine("Guesses = @Guesses,");
            sb.AppendLine("IsAccepted = @IsAccepted,");
            sb.AppendLine("Score = @Score,");
            sb.AppendLine("LastActivityDate = @LastActivityDate,");
            sb.AppendLine("LastEditDate = @LastEditDate,");
            sb.AppendLine("CreationDate = @CreationDate,");
            sb.AppendLine("AnswerID = @AnswerID,");
            sb.AppendLine("QuestionID = @QuestionID,");
            sb.AppendLine("Body = @Body");
            sb.AppendLine($"where ID = {item.ID}");
            return sb.ToString();
        }

    }
}