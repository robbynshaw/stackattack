using stackattack.Persistence;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace stackattack.Users
{
    public class UserStore : IUserStore
    {
        public string CookieName { get { return "stackattackID"; } }

        private FileBasedStorage storage;

        public UserStore(FileBasedStorage storage)
        {
            this.storage = storage;
        }

        public IUser Create()
        {
            IDataStore<User> table;

            using (SQLiteConnection con = this.storage.GetUserTable(out table))
            {
                User user = new User();
                table.Save(con, user);
                return user;
            }
        }

        public IUser Get(int id)
        {
            IDataStore<User> table;

            using (SQLiteConnection con = this.storage.GetUserTable(out table))
            {
                User user = table.Get(con, id);
                return user;
            }
        }

        public void AddScore(int userID, int score)
        {
            IDataStore<User> table;

            using (SQLiteConnection con = this.storage.GetUserTable(out table))
            {
                User user = table.Get(con, userID);
                if (user != null)
                {
                    user.HighScore += score;
                    table.Save(con, user);
                }
            }
        }
    }
}