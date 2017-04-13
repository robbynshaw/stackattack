using stackattack.Answers;
using stackattack.Core;
using stackattack.External;
using System;
using System.Collections.Generic;
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
        private RandomizedStore<IQuestion> randomQuestions = new RandomizedStore<IQuestion>();
        private StackOverflowAPI soApi = new StackOverflowAPI();

        private List<Question> allQuestions = new List<Question>();

        public int MaxQuestions { get; private set; }

        public QuestionStore(int maxQuestions)
        {
            this.MaxQuestions = maxQuestions;

            // Initial load
            LoadMore();
        }

        private void LoadMore()
        {
            if (allQuestions.Count() < this.MaxQuestions)
            {
                IEnumerable<Question> questions = this.soApi
                    .GetQuestionsWithAcceptedAnswers(5, 5);

                randomQuestions.Insert(questions);
                allQuestions.AddRange(questions);
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
    }
}