using stackattack.Core;
using stackattack.Questions;
using stackattack.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace stackattack.Controllers
{
    public class UserController : ApiController
    {
        private IQuestionStore questionStore;
        private IUserStore userStore;

        public UserController()
        {
            // Defaults
            this.questionStore = Cache.QuestionStore;
            this.userStore = Cache.UserStore;
        }

        public UserController(IQuestionStore questionStore, IUserStore userStore)
        {
            this.questionStore = questionStore;
            this.userStore = userStore;
        }

        // GET api/<controller>/5
        public IUser Get(int id)
        {
            return this.userStore.Get(id);
        }

        // GET api/<controller>/CheckScore
        public IGuessResponse CheckScore(int userID, int answerID)
        {
            IGuessResponse response = this.questionStore.CheckAnswer(answerID);
            this.userStore.AddScore(userID, response.Score);
            return response;
        }
    }
}