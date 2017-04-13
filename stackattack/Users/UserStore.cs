using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stackattack.Users
{
    public class UserStore : IUserStore
    {
        public string CookieName { get { return "stackattackID"; } }

        public IUser Create()
        {
            throw new NotImplementedException();
        }

        public IUser Get(int id)
        {
            throw new NotImplementedException();
        }

        public UserStore()
        {

        }
    }
}