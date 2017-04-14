using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stackattack.Core
{
    public interface IConfig
    {
        int MaxQuestions { get; }
        string SQLiteDatabasePath { get; }
    }
}
