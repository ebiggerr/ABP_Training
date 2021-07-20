using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace demo.Controllers
{
    public abstract class demoControllerBase: AbpController
    {
        protected demoControllerBase()
        {
            LocalizationSourceName = demoConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
