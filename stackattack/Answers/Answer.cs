using stackattack.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using stackattack.Core;
using System.Data.SQLite;

namespace stackattack.Answers
{
    public class Answer : IAnswer, IDbReadable
    {
        public long ID { get; set; }
        public long DBQuestionID { get; set; }
        public int GuessScore { get; set; }
        public int Guesses { get; set; }

#region StackOverflow Properties
        public bool IsAccepted { get; set; }
        public int Score { get; set; }
        public DateTime LastActivityDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public DateTime CreationDate { get; set; }
        public long AnswerID { get; set; }
        public long QuestionID { get; set; }
        public string Body { get; set; }
#endregion

        public void Read(DbDataReader r)
        {
            SQLiteDataReader rdr = r as SQLiteDataReader;

            for (int i = 0; i < rdr.FieldCount; i++)
            {
                switch (rdr.GetName(i))
                {
                    case "ID":
                        this.ID = rdr.GetInt64(i);
                        break;
                    case "DBQuestionID":
                        this.DBQuestionID = rdr.GetInt64(i);
                        break;
                    case "GuessScore":
                        this.GuessScore = rdr.GetInt32(i);
                        break;
                    case "Guesses":
                        this.Guesses = rdr.GetInt32(i);
                        break;
                    case "IsAccepted":
                        this.IsAccepted = rdr.GetBoolean(i);
                        break;
                    case "Score":
                        this.Score = rdr.GetInt32(i);
                        break;
                    case "LastActivityDate":
                        this.LastActivityDate = rdr.GetDateTime(i);
                        break;
                    case "LastEditDate":
                        this.LastEditDate = rdr.GetDateTime(i);
                        break;
                    case "CreationDate":
                        this.CreationDate = rdr.GetDateTime(i);
                        break;
                    case "AnswerID":
                        this.AnswerID = rdr.GetInt64(i);
                        break;
                    case "QuestionID":
                        this.QuestionID = rdr.GetInt64(i);
                        break;
                    case "Body":
                        this.Body = rdr.GetString(i);
                        break;
                }
            }
        }
    }
}