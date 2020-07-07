using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Bit.Owin;

namespace CrmSolution.Server.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await BuildWebHost(args)
                .RunAsync();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            BitWebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
