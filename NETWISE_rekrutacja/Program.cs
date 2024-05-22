using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;

namespace ConsoleApplication{
    public class Program{
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection().AddLogging();

            var container = new Container();
            container.Configure(config =>
            {
                config.Scan(_ =>
                            {
                                _.AssemblyContainingType(typeof(Program));
                                _.WithDefaultConventions();
                            });
                config.Populate(services);
            });

            var serviceProvider = container.GetInstance<IServiceProvider>();

            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);
            
            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            try
            {
                logger.LogDebug("Starting application");

                var connectService = serviceProvider.GetService<IConnectService>();              

                var writeService = serviceProvider.GetService<IWriteService>();

                writeService.SaveResponse(await connectService.GetAsync());

                logger.LogDebug("All done!");
            }
            catch(Exception ex)
            {
                logger.LogDebug(ex.Message);
            }
        }
    }
}
