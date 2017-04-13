using stackattack.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace stackattack.Answers
{
    public class Answer : IAnswer, IDbReadable
    {
        public int ID { get; set; }
        public int DBQuestionID { get; set; }
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

        public void Read(DbDataReader rdr)
        {
            throw new NotImplementedException();
        }
    }
}