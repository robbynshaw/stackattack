using stackattack.Answers;
using stackattack.Core;
using stackattack.External;
using stackattack.Persistence;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace stackattack.Questions
{
    /// <summary>
    /// Repository for questions. Ensure to use a single instance for
    /// proper randomization.
    /// </summary>
    public class QuestionStore : IQuestionStore
    {
        private int fetchSize = 5;
        private int anwerCount = 5;

        private FileBasedStorage storage;
        private RandomizedStore<IQuestion> randomQuestions = new RandomizedStore<IQuestion>();
        private StackOverflowAPI soApi = new StackOverflowAPI();

        private List<Question> allQuestions = new List<Question>();
        private List<Answer> allAnswers = new List<Answer>();

        public int MaxQuestions { get; private set; }

        public QuestionStore(FileBasedStorage storage, int maxQuestions)
        {
            this.storage = storage;
            this.MaxQuestions = maxQuestions;

            // Initial load
            LoadFromDB();
            LoadFromAPI();
        }

        private void Insert(IEnumerable<Question> items)
        {
            if (items != null)
            {
                // TODO should be dicts for performance
                this.randomQuestions.Insert(items);
                this.allQuestions.AddRange(items);
                this.allAnswers.AddRange(items.SelectMany(q => q.Answers));
            }
        }

        private void Insert(Question item)
        {
            if (item != null)
            {
                this.randomQuestions.Insert(item);
                this.allQuestions.Add(item);
                this.allAnswers.AddRange(item.Answers);
            }
        }

        private void LoadFromDB()
        {
            IDataStore<Question> questionTable;
            IDataStore<Answer> answerTable;
            IEnumerable<Question> questions = null;
            IEnumerable<Answer> answers = null;
            SQLiteConnection con = null;

            using (con = this.storage.GetQuestionTable(out questionTable))
            {
                if (questionTable != null)
                {
                    questions = questionTable.GetAll(con);
                }
            }

            using (con = this.storage.GetAnswerTable(out answerTable))
            {
                if (answerTable != null)
                {
                    answers = answerTable.GetAll(con);
                }
            }

            if (questions != null && answers != null)
            {
                Question.MergeQuestionsAndAnswers(ref questions, answers);
                Insert(questions);
            }
        }

        private void LoadFromAPI()
        {
            int page = 0;

            while (this.allQuestions.Count < this.MaxQuestions)
            {
                // Don't fetch too much
                int toFetch = this.MaxQuestions - this.allQuestions.Count;
                toFetch = toFetch > this.fetchSize ? this.fetchSize : toFetch;

                IEnumerable<Question> questions = this.soApi
                    .GetQuestionsWithAcceptedAnswers(toFetch, this.anwerCount, ++page);

                if (questions != null)
                {
                    // Will also insert
                    Save(questions);
                }
            }
        }

        /// <summary>
        /// Expects fresh questions with answers from the api
        /// </summary>
        /// <param name="questions"></param>
        private void Save(IEnumerable<Question> questions)
        {
            IDataStore<Question> questionTable;
            IDataStore<Answer> answerTable;

            using (SQLiteConnection con = this.storage.GetQuestionTable(out questionTable))
            {
                answerTable = this.storage.GetAnswerTable();

                foreach (Question q in questions)
                {
                    // Will fail if no accepted answer
                    if (q.CalculateAnswerScores())
                    {
                        if (!this.allQuestions.Any(aq => aq.QuestionID == q.QuestionID))
                        {
                            questionTable.Save(con, q);

                            foreach (Answer a in q.Answers)
                            {
                                a.DBQuestionID = q.ID;
                                answerTable.Save(con, a);
                            }
                        }
                        Insert(q);
                    }
                }
            }
        }

        private void RegisterGuess(Question question, Answer answer)
        {
            IDataStore<Question> questionTable;

            question.Guesses++;
            question.LastGuess = DateTime.Now;

            answer.Guesses++;

            using (SQLiteConnection con = this.storage.GetQuestionTable(out questionTable))
            {
                questionTable.Save(con, question);

                this.storage.GetAnswerTable().Save(con, answer);
            }
        }

        public IQuestion GetRandom()
        {
            return this.randomQuestions.Get();
        }

        public IEnumerable<IQuestion> GetRandom(int count)
        {
            return this.randomQuestions.Get(count);
        }

        public IEnumerable<IQuestion> GetRecentlyGuessed(int count)
        {
            return this.allQuestions
                .OrderByDescending(q => q.LastGuess)
                .Take(count);
        }

        public IGuessResponse CheckAnswer(int answerID)
        {
            int score = 0;
            bool success = false;

            Answer answer = this.allAnswers.FirstOrDefault(a => a.ID == answerID);
            Question question = this.allQuestions.FirstOrDefault(q => q.ID == answer.DBQuestionID);

            if (question != null && answer != null)
            {
                RegisterGuess(question, answer);

                score = answer.GuessScore;
                success = answer.IsAccepted;
            }
            return new GuessResponse(success, score);
        }
    }
}