using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using demo.Configuration.Dto;

namespace demo.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : demoAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
