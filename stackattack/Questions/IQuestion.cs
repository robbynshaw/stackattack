using stackattack.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stackattack.Questions
{
    public interface IQuestion : IQuestionSummary
    {
        long ViewCount { get; }
        DateTime LastActivityDate { get; }
        DateTime CreationDate { get; }
        DateTime LastEditDate { get; }
        string Body { get; }

        List<Answer> Answers { get; }
    }
}
