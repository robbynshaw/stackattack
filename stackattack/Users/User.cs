using stackattack.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace stackattack.Users
{
    public class User : IUser, IDbReadable
    {
        public int ID { get; set; }
        public string HighScore { get; internal set; }

        public User() { }

        public User(int id)
        {
            this.ID = id;
        }

        public void Read(DbDataReader rdr)
        {
            throw new NotImplementedException();
        }
    }
}