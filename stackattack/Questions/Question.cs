using stackattack.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using stackattack.Answers;
using stackattack.Core;
using System.Data.SQLite;

namespace stackattack.Questions
{
    public class Question : IQuestion, IDbReadable
    {
        private List<Answer> answers = new List<Answer>();

        public long ID { get; set; }

        public DateTime LastGuess { get; set; }
        public long Guesses { get; set; }

        public List<string> Tags { get; set; }
        public bool IsAnswered { get; set; }
        public long ViewCount { get; set; }
        public long AcceptedAnswerID { get; set; }
        public int Score { get; set; }
        public DateTime LastActivityDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public long QuestionID { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public virtual List<Answer> Answers
        {
            get { return this.answers; }
            set { this.answers = value; }
        }

        private void CalculateAnswerScores(int acceptedScore)
        {
            if (this.Answers != null)
            {
                foreach (Answer answer in this.Answers)
                {
                    if (answer.IsAccepted)
                    {
                        // Correct guesses get 100
                        answer.GuessScore = 100;
                    }
                    else
                    {
                        // Incorrect guesses get the percentage of the accepted
                        // score up to 75
                        if (answer.Score > 0)
                        {
                            double percent = (double)answer.Score / (double)acceptedScore;
                            answer.GuessScore = (int)(percent * 100);
                            if (answer.GuessScore > 75)
                            {
                                answer.GuessScore = 75;
                            }
                        }
                        else
                        {
                            answer.GuessScore = 0;
                        }
                    }
                }
            }
        }

        public bool CalculateAnswerScores()
        {
            // Get the score of the accepted answer for comparison
            Answer acceptedAnswer = this.Answers
                .Where(a => a.IsAccepted)
                .FirstOrDefault();

            if (acceptedAnswer != null)
            {
                CalculateAnswerScores(acceptedAnswer.Score);
                return true;
            }
            return false;
        }

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
                    case "LastGuess":
                        this.LastGuess = rdr.GetDateTime(i);
                        break;
                    case "Guesses":
                        this.Guesses = rdr.GetInt32(i);
                        break;
                    case "IsAnswered":
                        this.IsAnswered = rdr.GetBoolean(i);
                        break;
                    case "ViewCount":
                        this.ViewCount = rdr.GetInt32(i);
                        break;
                    case "AcceptedAnswerID":
                        this.AcceptedAnswerID = rdr.GetInt32(i);
                        break;
                    case "Score":
                        this.Score = rdr.GetInt32(i);
                        break;
                    case "LastActivityDate":
                        this.LastActivityDate = rdr.GetDateTime(i);
                        break;
                    case "CreationDate":
                        this.CreationDate = rdr.GetDateTime(i);
                        break;
                    case "LastEditDate":
                        this.LastEditDate = rdr.GetDateTime(i);
                        break;
                    case "QuestionID":
                        this.QuestionID = rdr.GetInt64(i);
                        break;
                    case "Link":
                        this.Link = rdr.GetString(i);
                        break;
                    case "Title":
                        this.Link = rdr.GetString(i);
                        break;
                    case "Body":
                        this.Link = rdr.GetString(i);
                        break;
                }
            }
        }

        public static void MergeQuestionsAndAnswers(ref IEnumerable<Question> questions, IEnumerable<Answer> answers)
        {
            // Create dict for sorting later
            IDictionary<long, Question> questionDict = questions.ToDictionary(q => q.QuestionID);

            // Add them to the questions. They do not come in order.
            foreach (Answer answer in answers)
            {
                Question q;
                if (questionDict.TryGetValue(answer.QuestionID, out q))
                {
                    q.Answers.Add(answer);
                }
            }
        }
    }
}