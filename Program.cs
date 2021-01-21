
using harbor.Commands;
using harbor.Helpers;
using harbor.Shell;
using Microsoft.Extensions.DependencyInjection;

namespace harbor
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // Boot dependency injection
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<App>().RunAsync(args);
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            // Register here                        
            services.AddScoped<ServicesCollector>();
            services.AddScoped<CommandsCollector>();
            services.AddScoped<EnableCommand>();
            services.AddScoped<ListCommand>();
            services.AddScoped<StopCommand>();
            services.AddScoped<StartCommand>();
            services.AddScoped<DisableCommand>();
            services.AddScoped<HelpCommand>();
            services.AddScoped<LogCommand>();
            services.AddScoped<Docker>();
            services.AddTransient<App>();

            return services;
        }
    }
}
