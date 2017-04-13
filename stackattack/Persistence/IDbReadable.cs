using System.Data.Common;

namespace stackattack.Persistence
{
    interface IDbReadable
    {
        int ID { get; set; }
        void Read(DbDataReader rdr);
    }
}
