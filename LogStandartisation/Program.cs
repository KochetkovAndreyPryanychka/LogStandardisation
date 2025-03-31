using LogStandartisation;
using LogStandartisation.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        Configure(services);
        var serviceProvider = services.BuildServiceProvider();

        var formatter = serviceProvider.GetRequiredService<FormattingLogs>();
        await formatter.Run();
    }

    private static void Configure(IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        services.Configure<FilesOptions>(config.GetSection("Files"));
        services.AddSingleton<FormattingLogs>();
    }
}