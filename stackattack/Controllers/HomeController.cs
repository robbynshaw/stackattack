﻿using stackattack.Core;
using stackattack.Models;
using stackattack.Users;
using System.Web.Mvc;

namespace stackattack.Controllers
{
    // This is not currently in use because it uses AngularJS,
    // and it was taking me too long, so I abandoned it.
    public class HomeController : BaseController
    {
        public HomeController() : base()
        {
        }

        public HomeController(IUserStore userStore) : base(userStore)
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
