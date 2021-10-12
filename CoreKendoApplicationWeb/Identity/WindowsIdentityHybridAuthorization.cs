using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using CoreKendoApplicationWeb.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CoreKendoApplicationWeb.Identity
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private const string UNAUTHORIZED_LOCATION = "~/Security/UnauthorizedUser";

        // Custom property
        public string AccessLevel { get; set; }

        // Because this function is required from the IAuthorizationFilter interface cannot make async
        // and can't use await operator for async functions so use .Result to cause synchronous execution
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (!AuthorizeCore(filterContext.HttpContext).Result)
            {
                HandleUnauthorizedRequest(filterContext);
            }
        }

        private async Task<bool> AuthorizeCore(HttpContext httpContext)
        {
            var userManager = httpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();

            string userName = httpContext.User.Identity.Name.Split('\\')[1];

            bool isAuthorized = false;

            var user = await userManager.FindByNameAsync(userName).ConfigureAwait(false);

            if (user != null)
            {
                // get the roles from the custom access level property set from the AuthorizeRolesAttribute attribute
                string[] rolesFromAccessLevel = AccessLevel.Split(",");

                // Want to check if the user is in ANY of the roles. Zero (0) indicates
                // no roles matches (unauthenticated). For each role found, increment by one.
                int roleSum = 0;
                foreach (string role in rolesFromAccessLevel)
                {
                    if (await userManager.IsInRoleAsync(user, role).ConfigureAwait(false))
                    {
                        roleSum++;
                    }
                }

                if (roleSum > 0)
                {
                    isAuthorized = true;
                }
            }

            return isAuthorized;
        }

        private void HandleUnauthorizedRequest(AuthorizationFilterContext filterContext)
        {
            filterContext.Result = new UnauthorizedResult();
            filterContext.Result = new RedirectResult(UNAUTHORIZED_LOCATION);
        }
    }
}
