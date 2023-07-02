using NLog;
using NLog.LayoutRenderers;
using NLog.Web;

class Program
{
    private static void Main(string[] args)
    {
        ConfigureNLogLayoutRenderer();
        CreateHostBuilder(args).Build().Run();
    }

    private static void ConfigureNLogLayoutRenderer()
    {
        LogManager.Setup().SetupExtensions(
            extensionsBuilder => extensionsBuilder.RegisterLayoutRenderer<SessionUuidLayoutRenderer>("sessionuuid")
        );
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