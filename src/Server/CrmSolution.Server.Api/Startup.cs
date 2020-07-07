using Bit.Core;
using Bit.Core.Contracts;
using Bit.Data;
using Bit.Data.Contracts;
using Bit.Data.EntityFrameworkCore.Implementations;
using Bit.Model.Implementations;
using Bit.OData.Contracts;
using Bit.Owin;
using Bit.Owin.Implementations;
using CrmSolution.Server.Api.Mappings;
using CrmSolution.Server.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Reflection;

[assembly: ODataModule("CrmSolutionV1")]

namespace CrmSolution.Server.Api
{
    public class Startup : AutofacAspNetCoreAppStartup, IAppModule, IAppModulesProvider
    {
        public Startup(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            AspNetCoreAppEnvironmentsProvider.Current.Init();
        }

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            DefaultAppModulesProvider.Current = this;

            return base.ConfigureServices(services);
        }

        public IEnumerable<IAppModule> GetAppModules()
        {
            yield return this;
        }

        public virtual void ConfigureDependencies(IServiceCollection services, IDependencyManager dependencyManager)
        {
            AssemblyContainer.Current.Init();

            dependencyManager.RegisterMinimalDependencies();

            dependencyManager.RegisterDefaultLogger(typeof(DebugLogStore).GetTypeInfo(), typeof(ConsoleLogStore).GetTypeInfo());

            dependencyManager.RegisterDefaultAspNetCoreApp();

            dependencyManager.RegisterMinimalAspNetCoreMiddlewares();

            dependencyManager.RegisterDefaultWebApiAndODataConfiguration();

            dependencyManager.RegisterWebApiMiddleware(webApiDependencyManager =>
            {
                webApiDependencyManager.RegisterWebApiMiddlewareUsingDefaultConfiguration();

                webApiDependencyManager.RegisterGlobalWebApiCustomizerUsing(httpConfiguration =>
                {
                    httpConfiguration.EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", $"Swagger-Api");
                        c.ApplyDefaultApiConfig(httpConfiguration);
                    }).EnableBitSwaggerUi();
                });
            });

            dependencyManager.RegisterODataMiddleware(odataDependencyManager =>
            {
                odataDependencyManager.RegisterGlobalWebApiCustomizerUsing(httpConfiguration =>
                {
                    httpConfiguration.EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", $"Swagger-Api");
                        c.ApplyDefaultODataConfig(httpConfiguration);
                    }).EnableBitSwaggerUi();
                });

                odataDependencyManager.RegisterWebApiODataMiddlewareUsingDefaultConfiguration();
            });

            dependencyManager.Register<IDbConnectionProvider, DefaultDbConnectionProvider<SqlConnection>>();
            dependencyManager.RegisterEfCoreDbContext<CrmSolutionDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(serviceProvider.GetRequiredService<IDbConnectionProvider>().GetDbConnection(serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("AppDbConnectionString"), rollbackOnScopeStatusFailure: true));
            });
            dependencyManager.RegisterRepository(typeof(EfCoreRepository<>).GetTypeInfo());
            dependencyManager.Register<IDesignTimeDbContextFactory<CrmSolutionDbContext>, CrmSolutionDbContextFactory>();

            dependencyManager.RegisterDtoEntityMapper();
            dependencyManager.RegisterMapperConfiguration<DefaultMapperConfiguration>();
            dependencyManager.RegisterMapperConfiguration<CrmSolutionMappings>();
        }
    }
}
