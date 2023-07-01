using FileWatcherService;
using LogLayout;
using NLog.Config;
using NLog.Web;

internal class Program
{
    private static void Main(string[] args)
    {
        ConfigureNLogLayoutRenderer();
        CreateHostBuilder(args).Build().Run();
    }

    private static void ConfigureNLogLayoutRenderer()
    {
        ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("sessionuuid", typeof(SessionUuidLayoutRenderer));
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;

                config.SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            })
            .UseNLog()
            .ConfigureServices(services =>
            {
                services.AddHostedService<Worker>();
            });
    }
}