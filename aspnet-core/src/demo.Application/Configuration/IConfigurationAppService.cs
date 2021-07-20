using System.Threading.Tasks;
using demo.Configuration.Dto;

namespace demo.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
