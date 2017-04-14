using System.Data.Common;

namespace stackattack.Persistence
{
    interface IDbReadable
    {
        long ID { get; set; }
        void Read(DbDataReader rdr);
    }
}
