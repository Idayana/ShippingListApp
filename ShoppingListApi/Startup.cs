using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShoppingListApi.Data;
using ShoppingListApi.Extensions;
using ShoppingListApi.Helpers;
using ShoppingListApi.Interfaces;

namespace ShoppingListApi
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
            services.AddControllers(op => {
                op.Filters.Add(typeof(ExceptionFilter));
            });
            services.AddScoped<DataContext>();
            services.AddDbContextPool<DataContext>((servicesProvider, options)=>
            {
                options.UseMySql("server=localhost;port=3306;database=shoppingList;user=root;password=root");
            });
            services.AddCors();
            services.AddAutoMapper(typeof(Startup));
            services.ConfigureDataDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
            /*var defaultConnection =
                $"Server={dbHost};Port={dbPort};Database={dbName};User={dbUser};Password={dbPassword};CharSet={charSet};";
            services.AddScoped<ApplicationDbContext>();
            services.AddDbContextPool<ApplicationDbContext>((servicesProvider, options) =>
            {
                options.UseMySql(defaultConnection,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                options.EnableSensitiveDataLogging();
            });
            
            optionsBuilder.UseMySQL("server=localhost;database=library;user=user;password=password");
            */