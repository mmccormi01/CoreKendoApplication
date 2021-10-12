using System.DirectoryServices.AccountManagement;

namespace CoreKendoApplicationService
{
    public class SecurityService
    {
        public ADUser SearchActiveDirectory(string userName)
        {
            using var context = new PrincipalContext(ContextType.Domain, "central");
            PrincipalSearcher searcher = new PrincipalSearcher(new UserPrincipal(context))
            {
                QueryFilter = new UserPrincipal(context)
                {
                    SamAccountName = userName
                }
            };

            var result = searcher.FindOne();

            if (result != null)
            {
                return new ADUser()
                {
                    UserName = result.SamAccountName,
                    DisplayName = result.DisplayName
                };
            }
            else
            {
                return null;
            }
        }
    }
}
