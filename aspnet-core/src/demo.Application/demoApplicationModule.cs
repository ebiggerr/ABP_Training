using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using demo.Authorization;

namespace demo
{
    [DependsOn(
        typeof(demoCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class demoApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<demoAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(demoApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
