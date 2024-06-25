using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using OnlineStore.Host.Api;
using OnlineStore.Host.Api.Activator;
using OnlineStore.Infrastructure.AppSetting;

public class Program
{
    public const string ApiPrefixV1 = "api/v1";
    public const string ApiPrefixV2 = "api/v2";

    public static void Main(string[] args)
    {
        var hostAddresses = ReadHostAddresses();
        CreateWebHostBuilder(args).UseUrls(hostAddresses.HostAddress).Build().Seed().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();

    private static HostAddresses ReadHostAddresses()
    {
        var appSettings = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json")
            .Build()
            .GetSection(nameof(HostAddresses))
            .Get<HostAddresses>();
        return appSettings;
    }
}