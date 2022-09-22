using AutoMapper;
using Catalog.API.Core.Entities;
using Catalog.API.DTO.Request;
using Catalog.API.Middleware;
using Catalog.API.Validation;
using Catalog.BAL;
using Catalog.Core.Interfaces;
using Catalog.Core.Settings;
using Catalog.DAL;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Catalog.API
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
            //Configure settings
            services.Configure<DatabaseSettings>(Configuration.GetSection(DatabaseSettings.SectionName));
            services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            //Configure dependencies
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton(typeof(ICatalogContext<>), typeof(CatalogContext<>));
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));


            //Configure AutoMapper
            services.AddSingleton(provider =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(Program).Assembly);
                    cfg.ConstructServicesUsing(type => ActivatorUtilities.CreateInstance(provider, type));
                });
                config.AssertConfigurationIsValid();
                return config.CreateMapper();
            });

            //Configure Controllers & FluentValidation
            services.AddControllers()
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Program>();
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = CustomValidationResponse.CreateResponse;
                });

            //Configure ExceptionHandler
            services.AddTransient<ExceptionHandlerMiddleware>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}