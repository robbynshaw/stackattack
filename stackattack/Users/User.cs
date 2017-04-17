using stackattack.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Data.SQLite;

namespace stackattack.Users
{
    public class User : IUser, IDbReadable
    {
        public long ID { get; set; }
        public int TotalScore { get; internal set; }

        public User()
        {
        }

        public User(int id) : this()
        {
            this.ID = id;
        }

        public void Read(DbDataReader r)
        {
            SQLiteDataReader rdr = r as SQLiteDataReader;

            for (int i = 0; i < rdr.FieldCount; i++)
            {
                switch (rdr.GetName(i))
                {
                    case "ID":
                        this.ID = rdr.GetInt64(i);
                        break;
                    case "TotalScore":
                        this.TotalScore = rdr.GetInt32(i);
                        break;
                }
            }
        }
    }
}