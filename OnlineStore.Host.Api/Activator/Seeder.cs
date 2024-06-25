using Microsoft.EntityFrameworkCore;
using OnlineShopStore.Infrastructure.Persistence.Context;

namespace OnlineShopStore.Host.Api.Activator
{
    public static class Seeder
    {
        public static IWebHost Seed(this IWebHost host)
        {
            try
            {
                using var scope = host.Services.CreateScope();

                var serviceProvider = scope.ServiceProvider;

                var databaseContext = serviceProvider.GetRequiredService<DatabaseContext>();

                databaseContext.Database.Migrate();
                return host;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return host;
            }
        }
    }
}
