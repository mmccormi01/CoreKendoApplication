using Microsoft.AspNetCore.Mvc;
using static CoreKendoApplicationWeb.Identity.HttpAuthRoleGroupConstants;
using CoreKendoApplicationWeb.Identity;

namespace CoreKendoApplicationWeb.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizeRoles(AccessLevel = HTTP_ROLE_GROUP_ALL)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
