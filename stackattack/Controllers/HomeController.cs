using stackattack.Core;
using stackattack.Models;
using stackattack.Users;
using System.Web.Mvc;

namespace stackattack.Controllers
{
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
