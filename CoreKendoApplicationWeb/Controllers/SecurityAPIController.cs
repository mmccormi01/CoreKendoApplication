using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreKendoApplicationService;
using CoreKendoApplicationWeb.Identity;
using CoreKendoApplicationWeb.Models;
using static CoreKendoApplicationWeb.Identity.HttpAuthRoleGroupConstants;
using static CoreKendoApplicationWeb.Identity.RoleConstants;

namespace CoreKendoApplicationWeb
{
    [Route("api/Security")]
    [ApiController]
    public class SecurityApiController : ControllerBase
    {
        // NOTE: With API endpoints using async functions always have a return type.
        // Using "async void" causes such things as the context disposing before execution
        // because of the dispatching of an asynchronous operation. Also doesn't play nicely
        // with Kendo UI components in event handling.

        private readonly SecurityService securityService = new SecurityService();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SecurityApiController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("GetSecurityUsers")]
        [AuthorizeRoles(AccessLevel = HTTP_ROLE_GROUP_ALL)]
        public async Task<List<SecurityUser>> GetSecurityUsers()
        {
            List<ApplicationUser> identityUsers = await _userManager.Users.ToListAsync().ConfigureAwait(false);
            List<SecurityUser> users = new List<SecurityUser>();

            foreach (ApplicationUser identityUser in identityUsers)
            {
                //By default, assign the lowest privilege role(read - only)
                string roleName = ROLE_READ;

                //If there's a role present, replace the default with what we found.
                var rolesList = await _userManager.GetRolesAsync(identityUser).ConfigureAwait(false);

                roleName = rolesList.FirstOrDefault();

                SecurityUser userDTO = new SecurityUser
                {
                    UserId = identityUser.Id,
                    UserName = identityUser.UserName,
                    Email = identityUser.Email,
                    RoleName = roleName,
                    DisplayName = identityUser.CommonName
                };

                users.Add(userDTO);
            }

            //The role list in the ApplicationUser contains Guids. Wrapped everything in a DTO so we can have the role name and the Guid.
            LogHelper.Debug(string.Join(",", users.Select(x => $"{x.UserName}-{x.RoleName}").ToArray()));

            return users;
        }

        [HttpPost]
        [Route("UpdateUserRole")]
        [AuthorizeRoles(AccessLevel = HTTP_ROLE_GROUP_ADMIN)]
        public async Task<bool> UpdateUserRole([FromForm] SecurityUser user)
        {
            string originalRoleName = string.Empty;

            ApplicationUser identityUser = await _userManager.FindByNameAsync(user.UserName).ConfigureAwait(false);

            var rolesToDelete = (await _userManager.GetRolesAsync(identityUser).ConfigureAwait(false)).ToArray();

            if (rolesToDelete.Length > 0)
            {
                originalRoleName = string.Join(",", rolesToDelete);

                await _userManager.RemoveFromRolesAsync(identityUser, rolesToDelete).ConfigureAwait(false);
            }

            // create role if doesn't exist
            if (!await _roleManager.RoleExistsAsync(user.RoleName).ConfigureAwait(false))
            {
                var userRole = new IdentityRole
                {
                    Name = user.RoleName
                };
                await _roleManager.CreateAsync(userRole).ConfigureAwait(false);
            }

            var identityRole = await _roleManager.FindByNameAsync(user.RoleName).ConfigureAwait(false);
            var updateRolesResult = await _userManager.AddToRolesAsync(identityUser, new List<string>() { identityRole.Name }).ConfigureAwait(false);

            LogHelper.Info("Updated user " + user.UserId + " from role " + originalRoleName + " to " + identityRole.Name);

            return updateRolesResult.Succeeded;
        }

        [HttpPost]
        [Route("AddSecurityUser")]
        [AuthorizeRoles(AccessLevel = HTTP_ROLE_GROUP_ADMIN)]
        public async Task<bool> AddSecurityUser(SecurityUser user)
        {
            ApplicationUser identityUser = new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.Email,
                CommonName = user.DisplayName
            };

            var createUserResult = await _userManager.CreateAsync(identityUser, Identity.UserConstants.NO_PASSWORD).ConfigureAwait(false);

            //Add the user role specified
            if (createUserResult.Succeeded)
            {
                // create role if doesn't exist
                if (!await _roleManager.RoleExistsAsync(user.RoleName).ConfigureAwait(false))
                {
                    var userRole = new IdentityRole
                    {
                        Name = user.RoleName
                    };
                    await _roleManager.CreateAsync(userRole).ConfigureAwait(false);
                }

                await _userManager.AddToRolesAsync(identityUser, new List<string>() { user.RoleName }).ConfigureAwait(false);

                LogHelper.Info("Added security user : " + user.UserName + " with role " + user.RoleName);
            }

            return createUserResult.Succeeded;
        }

        [HttpPost]
        [Route("DeleteSecurityUser")]
        [AuthorizeRoles(AccessLevel = HTTP_ROLE_GROUP_ADMIN)]
        public async Task<bool> DeleteSecurityUserByUserName([FromForm] SecurityUser user)
        {
            // Get the actual identity user
           ApplicationUser identityUser = await _userManager.FindByNameAsync(user.UserName).ConfigureAwait(false);

            if (identityUser == null)
            {
                LogHelper.Warn("Unable to delete user " + user.UserName + " because they don't exist.");
                return false;
            }

            var roles = await _userManager.GetRolesAsync(identityUser).ConfigureAwait(false);

            // remove the role from the user so we don't have orphan records in the UserRoles table
            await _userManager.RemoveFromRolesAsync(identityUser, roles.ToArray()).ConfigureAwait(false);

            // now delete the user from the User table
            var deleteUserResult = await _userManager.DeleteAsync(identityUser).ConfigureAwait(false);

            LogHelper.Info("Deleted user " + user.UserName);

            return deleteUserResult.Succeeded;
        }

        [HttpGet]
        [Route("SearchActiveDirectory")]
        [AuthorizeRoles(AccessLevel = HTTP_ROLE_GROUP_ALL)]
        public ADUser SearchActiveDirectory(string userName)
        {
            return securityService.SearchActiveDirectory(userName);
        }

        [HttpGet]
        [Route("IsUserRoleCurrent")]
        [AuthorizeRoles(AccessLevel = HTTP_ROLE_GROUP_ALL)]
        public async Task<bool> IsUserRoleCurrent(string userName, bool isUserAdmin, bool isUserReadWrite, bool isUserReadOnly)
        {
            ApplicationUser identityUser = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);

            string userRole = (await _userManager.GetRolesAsync(identityUser).ConfigureAwait(false)).ToArray()[0];

            if (userRole == ROLE_ADMIN && isUserAdmin)
            {
                return true;
            }
            else if (userRole == ROLE_READ && isUserReadOnly)
            {
                return true;
            }
            else if (userRole == ROLE_WRITE && isUserReadWrite)
            {
                return true;
            }

            return false;
        }
    }
}
