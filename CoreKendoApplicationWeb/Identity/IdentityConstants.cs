
namespace CoreKendoApplicationWeb.Identity
{
    // RoleConstants are the discrete roles assigned to a user in the database. Each user receives one role.
    public static class RoleConstants
    {
        public const string ROLE_ADMIN = "Admin";
        public const string ROLE_READ = "Read";
        public const string ROLE_WRITE = "Write";
    }

    // HttpAuthRoleConstants are the values paired with the AuthorizeOSCRoles attribute to protect HTTP endpoints.
    // They represenent a set of roles (an OR operation) that can access a particular functionality. Note that Admin is in every group.
    public static class HttpAuthRoleGroupConstants
    {
        public const string HTTP_ROLE_GROUP_ADMIN = RoleConstants.ROLE_ADMIN;
        public const string HTTP_ROLE_GROUP_ALL = RoleConstants.ROLE_ADMIN + "," + RoleConstants.ROLE_READ + "," + RoleConstants.ROLE_WRITE;
        public const string HTTP_ROLE_GROUP_READ = RoleConstants.ROLE_ADMIN + "," + RoleConstants.ROLE_READ;
    }

    public static class UserConstants
    {
        public const string NO_PASSWORD = "NOPASSWORD";
    }

    public static class DefaultAuthenticationTypes
    {
        public const string ApplicationCookie = "ApplicationCookie";
    }
}
