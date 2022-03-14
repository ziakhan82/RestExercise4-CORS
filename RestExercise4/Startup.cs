using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestExercise4.Managers;
using RestExercise4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestExercise4
{
    public class Startup
    {
        public const string AllowAllPolicyName = "allowAll";
        public const string AllowOnlyGetMethodPolicyName = "allowOnlyGetMethod";
        public const string AllowOnlyZealandOriginPolicyName = "allowOnlyZealandOrigin";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddDbContext<ItemContext>(opt => opt.UseInMemoryDatabase("ItemDB"));
            services.AddDbContext<ItemContext>(opt => opt.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ItemDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
            services.AddTransient<ItemsManager>();

            //Having a policy that allows all
            services.AddCors(options => options.AddPolicy(AllowAllPolicyName,
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

            //A policy that only allows GET, but everything else
            //We don't use this in this application, this is just an example
            services.AddCors(options => options.AddPolicy(AllowOnlyGetMethodPolicyName,
                builder => builder.AllowAnyOrigin()
                .WithMethods("GET")
                .AllowAnyHeader()));

            //A policy that only allow requests coming from zealand.dk
            services.AddCors(options => options.AddPolicy(AllowOnlyZealandOriginPolicyName,
                builder => builder.WithOrigins("https://zealand.dk")
                .AllowAnyMethod()
                .AllowAnyHeader()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //What the default policy should be
            app.UseCors("allowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
