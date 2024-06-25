using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using OnlineStore.Infrastructure.AppSetting;
using OnlineStore.Infrastructure.Persistence.Context;
using System.Reflection;
using OnlineStore.Domain.DomainModel.Repositories;
using OnlineStore.Host.Api.Middlewares;
using OnlineStore.Infrastructure.Persistence.Repositories;
using OnlineStore.Infrastructure.Persistence.UnitOfWork;

namespace OnlineStore.Host.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            ConfigureDatabaseContexts(services);
            ConfigureAppSettingsFiles(services);
            ConfigureApplicationSwagger(services);
            ConfigureMediator(services);
            ConfigureUnitOfWork(services);
            ConfigureRepositories(services);
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple  Online Shop ");
                });
            }
                
            app.UseRouting();
            app.UseMiddleware<ApplicationExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureAppSettingsFiles(IServiceCollection services)
        {
            services.Configure<HostAddresses>(Configuration.GetSection(nameof(HostAddresses)));
        }
        private void ConfigureDatabaseContexts(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Db")));
        }

        private void ConfigureApplicationSwagger( IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Simple  Online Shop", Version = "v1" });

            });
        }

        private void ConfigureMediator(IServiceCollection services)
        {

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


        }

        private  void ConfigureUnitOfWork(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        private void ConfigureRepositories(IServiceCollection services)
        {

            services.AddScoped<IProductRepository, ProductRepository>();

        }

    }
}
