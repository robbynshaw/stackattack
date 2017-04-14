using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stackattack.Questions
{
    public interface IQuestionSummary
    {
        long ID { get; }

        DateTime LastGuess { get; }
        long Guesses { get; }
        string Title { get; }
    }
}
