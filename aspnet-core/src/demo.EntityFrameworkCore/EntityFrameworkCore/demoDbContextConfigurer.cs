using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace demo.EntityFrameworkCore
{
    public static class demoDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<demoDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<demoDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
