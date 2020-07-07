using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CrmSolution.Server.Data
{
    public class CrmSolutionDbContextFactory : IDesignTimeDbContextFactory<CrmSolutionDbContext>
    {
        public IConfiguration Configuration { get; set; }

        public CrmSolutionDbContext CreateDbContext(string[] args)
        {
            Configuration ??= (CrmSolutionConfigurationProvider.GetConfiguration());

            return new CrmSolutionDbContext(new DbContextOptionsBuilder<CrmSolutionDbContext>()
                .UseSqlServer(Configuration.GetConnectionString("AppDbConnectionString")).Options);
        }
    }
}
