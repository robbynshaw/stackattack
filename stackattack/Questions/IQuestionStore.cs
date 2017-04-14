using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stackattack.Questions
{
    public interface IQuestionStore
    {
        IQuestion GetRandom();
        IEnumerable<IQuestion> GetRandom(int count);
        IEnumerable<IQuestion> GetRecentlyGuessed(int count);
        IGuessResponse CheckAnswer(int questionID);
        IQuestion Get(int id);
    }
}