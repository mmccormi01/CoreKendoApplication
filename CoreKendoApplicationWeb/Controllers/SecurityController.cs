using Microsoft.AspNetCore.Mvc;
using CoreKendoApplicationWeb.Identity;
using static CoreKendoApplicationWeb.Identity.HttpAuthRoleGroupConstants;

namespace CoreKendoApplicationWeb.Controllers
{
    public class SecurityController : Controller
    {
        // Note - this view must not be controlled by roles. All users authenticated on the PC domain should
        // be able to view it. The application sends two types of unauthenticated users here - those who have valid
        // domain credentials but NO role in the system and those who have valid domain credentials but the WRONG role for
        // whatever they are trying to access.
        public ActionResult UnauthorizedUser()
        {
            return View("Unauthorized");
        }

        [AuthorizeRolesAttribute(AccessLevel = HTTP_ROLE_GROUP_ADMIN)]
        public ActionResult ManageUserRoles()
        {
            return View("UserAdmin");
        }
    }
}
