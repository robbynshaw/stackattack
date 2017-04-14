using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stackattack.Users
{
    public interface IUserStore
    {
        IUser Get(int id);
        IUser Create();
        string CookieName { get; }

        void AddScore(int userID, int score);
    }
}
