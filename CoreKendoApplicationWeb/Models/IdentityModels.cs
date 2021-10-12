using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using CoreKendoApplicationWeb.Identity;

namespace CoreKendoApplicationWeb.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = new ClaimsIdentity(await manager.GetClaimsAsync(this).ConfigureAwait(false), DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("CommonName", this.CommonName));

            return userIdentity;
        }

        public string CommonName { get; set; }
    }

    public static class IdentityHelper
    {
        public static string GetProfilePicture(this IIdentity identity)
        {
            return identity is ClaimsIdentity claimIdent
                && claimIdent.HasClaim(c => c.Type == "CommonName")
                ? claimIdent.FindFirst("CommonName").Value
                : string.Empty;
        }
    }
}
