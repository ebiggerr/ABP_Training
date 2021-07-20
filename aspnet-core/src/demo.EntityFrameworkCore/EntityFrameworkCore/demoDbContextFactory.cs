using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using demo.Configuration;
using demo.Web;

namespace demo.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class demoDbContextFactory : IDesignTimeDbContextFactory<demoDbContext>
    {
        public demoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<demoDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            demoDbContextConfigurer.Configure(builder, configuration.GetConnectionString(demoConsts.ConnectionStringName));

            return new demoDbContext(builder.Options);
        }
    }
}
