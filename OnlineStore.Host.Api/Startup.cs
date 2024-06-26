﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;
using OnlineShopStore.Domain.DomainModel.Models.User;
using OnlineShopStore.Domain.DomainModel.Repositories;
using OnlineShopStore.Host.Api.Middlewares;
using OnlineShopStore.Infrastructure.AppSetting;
using OnlineShopStore.Infrastructure.Persistence.UnitOfWork;
using OnlineShopStore.Infrastructure.Persistence.Repositories;
using OnlineShopStore.Infrastructure.Persistence.Context;
using MediatR;
using OnlineShopStore.Application.Command.Contracts.Commands;
using Microsoft.Extensions.DependencyInjection;
using OnlineShopStore.Application.Command.Implementation;
using OnlineShopStore.Application.Query.Implementation;
using OnlineShopStore.Infrastructure.Cache;

namespace OnlineShopStore.Host.Api
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
            ConfigureCacheInfraStructure(services);
            ConfigureRedis(services);
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

        private void ConfigureApplicationSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Simple  Online Shop", Version = "v1" });

            });
        }

        private void ConfigureMediator(IServiceCollection services)
        {

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(BuyCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductByIdQueryHandler).Assembly));

        }

        private void ConfigureUnitOfWork(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        private void ConfigureRepositories(IServiceCollection services)
        {

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

        }

        private void ConfigureCacheInfraStructure(IServiceCollection services)
        {
            services.AddScoped<ICacheProvider, RedisCacheProvider>();
        }

        private void ConfigureRedis(IServiceCollection services)
        {
            services.Configure<RedisConfig>
                (Configuration.GetSection(nameof(RedisConfig)));
        }

    }
}
