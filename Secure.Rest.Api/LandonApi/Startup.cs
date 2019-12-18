using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandonApi.Filters;
using LandonApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NSwag.AspNetCore;
using Microsoft.EntityFrameworkCore;
using LandonApi.Services;
using AutoMapper;
using LandonApi.Infrastructure;

namespace LandonApi
{
    public class Startup
    {
        public IHostingEnvironment Env { get; set; }
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<HotelInfo>(
                Configuration.GetSection("Info"));

            services.AddScoped<IRoomService, DefaultRoomService>();

            //Ude in-memory database for quick dev and testing
            //TODO: Swap out for a real database in production
            services.AddDbContext<HotelApiDbContext>(
                options =>
                {
                    options.UseInMemoryDatabase("landonDb");
                });

            services
                .AddMvc(options =>
                {
                    options.Filters.Add<JsonExceptionFilter>();
                    options.Filters.Add<RequireHttpsOrCloseAttribute>();
                    options.Filters.Add<LinkRewritingFilter>();
                
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSwaggerDocument(configure => 
            {
                configure.SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
            });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyApp",
                    policy =>
                    {
                        if (Env.IsDevelopment())
                        {    
                            policy.WithOrigins("https://example.com");
                        }
                        else
                        {
                            policy.AllowAnyOrigin();
                        }
                    });
            });

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }
            else
            {
               app.UseHsts();
            }

            app.UseCors("AllowMyApp");
            //Not needed, we use a filter to disallow any http call
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
