using stackattack.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stackattack.Questions
{
    public interface IQuestion
    {
        long ID { get; }

        DateTime LastGuess { get; }
        long Guesses { get; }

        long ViewCount { get; }
        DateTime LastActivityDate { get; }
        DateTime CreationDate { get; }
        DateTime LastEditDate { get; }
        string Title { get; }
        string Body { get; }

        List<Answer> Answers { get; }
    }
}
