using Autofac;
using Domain.Product;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RefactorThis.Common;
using RefactorThis.Common.Configuration;
using RefactorThis.Configurations;
using RefactorThis.Mappers;
using RefactorThis.Validators;

namespace RefactorThis
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbConfig = new ConnectionsConfiguration(Configuration);
            services.AddSingleton<AppConfig>();
            services.AddSingleton<SwaggerConfiguration>();
            services.AddSingleton<BasicAuthConfiguration>();
            services.AddSingleton<ProductValidator>();
            services.AddSingleton<ProductOptionValidator>();
            
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AutoMapperConfig();
            services.AddDbContext<ProductContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlite(dbConfig.ProductConnectionString), ServiceLifetime.Scoped);
            services.AddAuthorization();
            services.SetupSwaggerDefinition(new SwaggerConfiguration(Configuration));
            services.SetupAuthorisation();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {

            builder.RegisterSingleton(Configuration).Named<IConfiguration>(ContainerBuilderExtensions.ConfigurationKey);
            builder.SetupDependencyResolver();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

            app.UseSwagger(new SwaggerConfiguration(Configuration));

            // global cors policy
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API");
                c.DisplayOperationId();
            });
        }
    }
}