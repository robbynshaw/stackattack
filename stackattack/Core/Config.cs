using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stackattack.Core
{
    public class Config : IConfig
    {
        public int MaxQuestions { get { return 100; } }
    }
}