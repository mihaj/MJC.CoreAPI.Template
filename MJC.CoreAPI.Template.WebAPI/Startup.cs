using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MJC.CoreAPI.Template.WebAPI.Persistence;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace MJC.CoreAPI.Template.WebAPI
{
    public class Startup
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public Startup(Microsoft.Extensions.Configuration.IConfiguration config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning(
                    o =>
                    {
                        o.AssumeDefaultVersionWhenUnspecified = true;
                        o.DefaultApiVersion = new ApiVersion(1, 0);
                        o.ReportApiVersions = true;
                        o.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader()
                            , new HeaderApiVersionReader(new string[] { "ver", "X-DummyAPI-version" }));
                    });

            services.AddMvc()
                    .AddMvcOptions(o =>
                    {
                        o.ReturnHttpNotAcceptable = true;
                        o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                        o.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
                    })
                    .AddJsonOptions(o =>
                    {
                        if (o.SerializerSettings.ContractResolver != null)
                        {
                            o.SerializerSettings.ContractResolver = new DefaultContractResolver
                            {
                                NamingStrategy = new CamelCaseNamingStrategy()
                            };
                        }
                    }
                );

            services.AddAutoMapper();

            services.AddTransient<IMailService, LocalMailService>();

            if (_env.EnvironmentName == "Test")
            {
                services.AddDbContext<DummyApiContext>(
                    options => options.UseInMemoryDatabase("DummyApiTest"));
            }
            else
            {
                services.AddDbContext<DummyApiContext>(
                    o => o.UseSqlServer(_config["ConnectionStrings:DummyApiContext"])
                    );
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment() || env.EnvironmentName == "Test")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();

            app.UseMvc();

            if (env.IsDevelopment() || env.EnvironmentName == "Test")
            {
                // Seed the database
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<DummyApiContext>();
                    seeder.Seed(Path.Combine(env.ContentRootPath, @"Data\Seed\Jsons\"));
                }
            }
        }
    }
}
