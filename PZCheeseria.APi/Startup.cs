using System;
using System.IO;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PZCheeseria.Api.Configuration;
using PZCheeseria.Api.Infrastructure;
using PZCheeseria.Api.Infrastructure.Middleware;
using PZCheeseria.BusinessLogic.Cheeses.Queries;
using PZCheeseria.Common;
using PZCheeseria.Infrastructure;
using PZCheeseria.Persistence;

namespace PZCheeseria.Api
{
    public class Startup
    {
        private const string cachePeriod = "604800";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            ConfigureSwagger(services);
          
            services.AddOptions();
            RegisterOptions(services, Configuration);
            
            services.AddDbContext<PZCheeseriaContext>((c,m )=> m
                .UseSqlServer(c.GetRequiredService<ConnectionStrings>().PZCheeseriaConnectionString));
            
            AddMediatR(services);
            ConfigureCors(services);
            services.AddSingleton<ITimeProvider, TimeProvider>();
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(m =>
            {
                m.SwaggerDoc("v1",new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PZCheeseria API",
                    Description = "An API to get cheese information"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                m.IncludeXmlComments(xmlPath);
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();

            }
            app.UseApiExceptionMiddleware();
            app.UseStaticFiles( new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Resources")),RequestPath = "/Resources",
                OnPrepareResponse = m => { m.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");}
            });
            
            ConfigureSwagger(app);

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "PZCheeseria API V1"); });
        }

        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                        ;
                    });
            });
        }

        
        private static  void AddMediatR(IServiceCollection services) => services.AddMediatR(typeof(GetAllCheesesQuery).GetTypeInfo().Assembly);
        private static void RegisterOptions(IServiceCollection services, IConfiguration configuration) => services.AddConfiguration(configuration);
    }
}