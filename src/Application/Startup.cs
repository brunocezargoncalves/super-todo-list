using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Interfaces;

namespace Application
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
            // services.AddCors();
            // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllersWithViews();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IToDoRepository, ToDoRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            // var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWT").GetSection("Key").Value);
            // services.AddAuthentication(x => {
            //     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // })
            // .AddJwtBearer(x =>
            // {
            //     x.RequireHttpsMetadata = false;
            //     x.SaveToken = true;
            //     x.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuerSigningKey = true,
            //         IssuerSigningKey = new SymmetricSecurityKey(key),
            //         ValidateIssuer = false,
            //         ValidateAudience = false
            //     };
            // });   

            services.AddAuthentication("Cookies")
                .AddCookie("Cookies", config =>
                {
                    config.Cookie.Name = "UserLoginCookie";
                    config.LoginPath = "/Login";
                    config.AccessDeniedPath = "/Login/Denied";
                    config.ExpireTimeSpan = TimeSpan.FromHours(8);
                    config.SlidingExpiration = true;
                });

            services.AddControllersWithViews();
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
            }

            app.UseStaticFiles();

            app.UseRouting();

            // app.UseCors(x => x
            // .AllowAnyOrigin()
            // .AllowAnyMethod()
            // .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            
            // app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllers();
                // endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=ToDo}/{action=Index}/{id?}");
            });
        }
    }
}
