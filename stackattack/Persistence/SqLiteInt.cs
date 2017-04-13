using System;
using System.Data.Common;

namespace stackattack.Persistence
{
    public class SqLiteInt : IDbReadable
    {
        // Not used
        public int ID { get; set; }

        public int Value { get; private set; }

        public void Read(DbDataReader rdr)
        {
            this.Value = rdr.GetInt32(0);
        }
    }
}