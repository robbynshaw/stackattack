using RestSharp;
using stackattack.Answers;
using stackattack.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stackattack.External
{
    public class StackOverflowAPI : APIClient
    {
        protected override Uri BaseUrl { get { return new Uri("https://api.stackexchange.com/"); } }

        public long QuotaMax { get; private set; }
        public long QuotaRemaining { get; private set; }

        private void UpdateQuotas<T>(StackOverflowItemsResponse<T> response)
        {
            if (response != null)
            {
                this.QuotaMax = response.QuotaMax;
                this.QuotaRemaining = response.QuotaRemaining;
            }
        }

        public IEnumerable<Question> GetQuestions(int count, int answerCount)
        {
            IRestClient client = GetClient();
            IRestRequest request = new RestRequest("2.2/search/advanced", Method.GET);

            request.AddParameter("site", "stackoverflow");
            request.AddParameter("page", 1);
            request.AddParameter("pagesize", count);
            request.AddParameter("order", "desc");
            request.AddParameter("sort", "activity");
            request.AddParameter("accepted", "true");
            request.AddParameter("answers", answerCount);
            request.AddParameter("filter", "withbody");

            var response = client.Execute<StackOverflowItemsResponse<Question>>(request);
            UpdateQuotas(response?.Data);
            return response?.Data.Items;
        }

        public IEnumerable<Answer> GetAnswersToQuestions(IEnumerable<long> questionIds)
        {
            IRestClient client = GetClient();

            // Create csv
            string allIds = questionIds
                .Select(id => id.ToString())
                .Aggregate((a, b) => a + ";" + b);

            IRestRequest request = new RestRequest($"2.2/questions/{allIds}/answers", Method.GET);

            request.AddParameter("site", "stackoverflow");
            request.AddParameter("order", "desc");
            request.AddParameter("sort", "activity");
            request.AddParameter("filter", "withbody");

            var response = client.Execute<StackOverflowItemsResponse<Answer>>(request);
            UpdateQuotas(response?.Data);
            return response?.Data.Items;
        }

        public IEnumerable<Question> GetQuestionsWithAcceptedAnswers(int count, int answerCount)
        {
            IEnumerable<Question> questions = null;

            questions = GetQuestions(count, answerCount);
            if (questions == null)
            {
                throw new Exception("Could not retrieve questions from StackOverflow");
            }

            // Create IEnumerable for query
            IEnumerable<long> questionIDs = questions.Select(q => q.QuestionID);

            // Create dict for sorting later
            IDictionary<long, Question> questionDict = questions.ToDictionary(q => q.QuestionID);

            // Get the answers to those questions
            IEnumerable<Answer> answers = GetAnswersToQuestions(questionIDs);
            if (answers == null)
            {
                throw new Exception("Could not retrieve answers from StackOverflow");
            }

            // Add them to the questions. They do not come in order.
            foreach (Answer answer in answers)
            {
                Question q;
                if (questionDict.TryGetValue(answer.QuestionID, out q))
                {
                    q.Answers.Add(answer);
                }
            }

            return questions;
        }
    }
}