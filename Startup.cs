using BOOK_STORE.Models;
using BOOK_STORE.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOOK_STORE
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

           // services.AddMvc();
            services.AddScoped<IBookStoreRepository<Author>, AuthorDbRepository>();
            services.AddScoped<IBookStoreRepository<Book>, BookDbRepository>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddDbContext<BookstoreDbcontext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString ("SqlCon"));

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            //app.UseMvc(route =>
            //{
            //    route.MapRoute("default", "{Controller =Book}/{action = Index}/{id ?}");
            //});

        }
    }
}
