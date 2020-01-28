using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealApp.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MealApp
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MealContext>(
                opts => opts.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")
                )
            );
            services.AddMvc(opts =>
            {
                opts.EnableEndpointRouting = false;
                opts.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    _ => "The field is required."); 
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}