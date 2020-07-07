using Bit.Data.EntityFrameworkCore.Implementations;
using CrmSolution.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace CrmSolution.Server.Data
{
    public class CrmSolutionDbContext : EfCoreDbContextBase
    {
        public CrmSolutionDbContext(DbContextOptions<CrmSolutionDbContext> options)
            : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}
