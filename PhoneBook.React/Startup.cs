using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBook.Contracts;
using PhoneBook.Data;
using PhoneBook.Repository;
using System.IO;

namespace PhoneBook.React
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
            string path = Directory.GetCurrentDirectory();
            //services.AddDbContext<PhoneBookDbContext>(options => options.UseSqlite(@"Data Source=D:\w\tests\PhoneBook.Solution\PhoneBook.React\PhoneBookDb.db"));
            services.AddDbContext<PhoneBookDbContext>(options => options.UseSqlite($"Data Source={ Path.Combine(path, "PhoneBookDb.db") }"));
            //; options.UseNpgsql(Configuration["ConnectionString:FrusDbContext"])

            services.AddScoped<IPhoneBookUserRepository, PhoneBookUserRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            services.AddCors(options =>
            {
                options.AddPolicy("Policy1", builder => builder.WithOrigins("https://localhost:44390/", "https://localhost:44390/"));
            });
            //services.AddCors(options => {
            //    options.AddDefaultPolicy(builder => {
            //        builder.WithOrigins("https://localhost:44390/", "https://localhost:44390/");
            //    });
            //});
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();
            app.UseCors(options => options.WithOrigins("https://localhost:44390/").AllowAnyMethod());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
