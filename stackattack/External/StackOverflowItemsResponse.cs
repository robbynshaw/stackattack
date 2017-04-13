using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stackattack.External
{
    public class StackOverflowItemsResponse<TItem>
    {
        public List<TItem> Items { get; set; }
        public bool HasMore { get; set; }
        public long QuotaMax { get; set; }
        public long QuotaRemaining { get; set; }
    }
}