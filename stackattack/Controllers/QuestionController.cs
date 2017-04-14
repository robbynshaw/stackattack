using stackattack.Core;
using stackattack.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace stackattack.Controllers
{
    public class QuestionController : ApiController
    {
        private IQuestionStore questionStore;

        public QuestionController()
        {
            // Defaults
            this.questionStore = Cache.QuestionStore;
        }

        public QuestionController(IQuestionStore questionStore)
        {
            this.questionStore = questionStore;
        }

        // GET api/<controller>/Get/5
        public IQuestion Get(int id)
        {
            return this.questionStore.Get(id);
        }

        // GET api/<controller>/Get?count=#
        public IEnumerable<IQuestionSummary> GetRecent(int count)
        {
            return this.questionStore.GetRecentlyGuessed(count);
        }

        // GET api/<controller>/GetRandom
        public IQuestion GetRandom()
        {
            return this.questionStore.GetRandom();
        }

        // GET api/<controller>/GetRandom?count=#
        public IEnumerable<IQuestionSummary> GetRandom(int count)
        {
            return this.questionStore.GetRandom(count);
        }
    }
}