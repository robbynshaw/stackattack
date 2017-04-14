using stackattack.Core;
using stackattack.Models;
using stackattack.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace stackattack.Controllers
{
    public class GameController : BaseController
    {
        public GameController() : base()
        {
        }

        public GameController(IUserStore userStore) : base(userStore)
        {
        }

        public ActionResult Index()
        {
            IUser user = this.GetCurrentUser();
            UserModel model = new UserModel(user);

            return View(model);
        }
    }
}