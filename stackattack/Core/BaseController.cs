using stackattack.Users;
using System;
using System.Web;
using System.Web.Mvc;

namespace stackattack.Core
{
    public class BaseController : Controller
    {
        protected IUserStore userStore;

        protected TimeSpan TicketExpiration
        {
            // 10 Years. Longer than any one browser lives.
            get { return TimeSpan.FromDays(365 * 10); }
        }

        public BaseController()
        {
            // Defaults
            this.userStore = Cache.UserStore;
        }

        public BaseController(IUserStore userStore)
        {
            this.userStore = userStore;
        }

        private HttpCookie CreateUserCookie(long id)
        {
            HttpCookie userCookie = new HttpCookie(this.userStore.CookieName, id.ToString());
            userCookie.Expires = DateTime.Now + this.TicketExpiration;
            return userCookie;
        }

        private string GetUserCookie()
        {
            return this.Request.Cookies[this.userStore.CookieName]?.Value;
        }

        private void SetUserCookie(long id)
        {
            this.Response.Cookies.Set(CreateUserCookie(id));
        }

        protected virtual IUser GetCurrentUser()
        {
            IUser user = null;

            string userID = GetUserCookie();
            if (!string.IsNullOrEmpty(userID))
            {
                int id;
                if (int.TryParse(userID, out id))
                {
                    user = this.userStore.Get(id);
                }
            }
            
            if (user == null)
            {
                user = this.userStore.Create();
                SetUserCookie(user.ID);
            }

            return user;
        }
    }
}