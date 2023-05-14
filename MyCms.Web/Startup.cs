using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCms.DataLayer.Context;
using MyCms.Services.Repositories;
using MyCms.Services.Services;

namespace MyCms.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddMvc();

            services.AddDbContext<MyCmsDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("MyCmsDbContext"))
            );

            services.AddTransient<ISkillsRepoitory, SkillsRepoitory>();
            services.AddTransient<IUserXRepository, UserXRepoitory>();
            services.AddTransient<IAboutRepository, AboutRepository>();
            services.AddTransient<IReciveInfoRepository, ReciveInfoRepository>();
            services.AddTransient<IGalleryRepository, GalleryRepository>();
            services.AddTransient<IConfigRepository, ConfigRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IRankRepository, RankRepository>();
            services.AddTransient<IPollsRepository, PollsRepository>(); 
            services.AddTransient<ISimplexRepository, SimplexRepository>();
            services.AddTransient<IStRepository, StRepository>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                     name: "LoginAreas",
                    template: "{area:exists}/{controller=UserX}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "PollsAreas",
                   template: "{area:exists}/{controller=Polls}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "SimplexAreas",
                   template: "{area:exists}/{controller=Simplex}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "AdminPanelAreas",
                    template: "{area:exists}/{controller=exists}/{action=exists}/{id?}");

                routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });

             app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
