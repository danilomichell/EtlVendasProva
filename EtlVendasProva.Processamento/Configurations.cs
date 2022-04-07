using EtlVendasProva.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EtlVendasProva.Processamento
{
    public static class Configurations
    {
        public static ServiceProvider Inject()
        {
            var serviceCollection = new ServiceCollection();

            var configuration = SetGeneralConfiguration(serviceCollection);

            SetDbContexts(serviceCollection, configuration);
            SetScopedServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }

        private static IConfiguration SetGeneralConfiguration(IServiceCollection serviceCollection)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .Build();

            serviceCollection.AddSingleton<IConfiguration>(configuration);

            return configuration;
        }

        private static void SetDbContexts(IServiceCollection serviceCollection, IConfiguration configuration)
        {

            var connectionVendas = configuration.GetConnectionString("VendasContext");
            serviceCollection.AddDbContextPool<VendasContext>(opts => opts.UseNpgsql(connectionVendas));

            var connectionVendasDw = configuration.GetConnectionString("VendasDwContext");
            serviceCollection.AddDbContextPool<VendasDwContext>(opts => opts.UseNpgsql(connectionVendasDw));
        }

        private static void SetScopedServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IProcessoEtl, ProcessoEtl>();
        }
    }
}
