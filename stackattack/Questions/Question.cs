using stackattack.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using stackattack.Answers;

namespace stackattack.Questions
{
    public class Question : IQuestion, IDbReadable
    {
        private List<Answer> answers = new List<Answer>();

        public int ID { get; set; }

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

        public List<Answer> Answers
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
                            answer.GuessScore = (acceptedScore / answer.Score) * 100;
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

        public void CalculateAnswerScores()
        {
            // Get the score of the accepted answer for comparison
            Answer acceptedAnswer = this.Answers
                .Where(a => a.IsAccepted)
                .FirstOrDefault();

            if (acceptedAnswer == null)
            {
                throw new Exception("A question got through without an accepted answer. Oh no!");
            }

            CalculateAnswerScores(acceptedAnswer.Score);
        }

        public void Read(DbDataReader rdr)
        {
            throw new NotImplementedException();
        }
    }
}