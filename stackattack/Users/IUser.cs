using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stackattack.Users
{
    public interface IUser
    {
        long ID { get; }
        int TotalScore { get; }
    }
}
