using Abp.Authorization;
using demo.Authorization.Roles;
using demo.Authorization.Users;

namespace demo.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
