using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeagueViewer.DataAccess;
using LeagueViewer.Entities;
using LeagueViewer.Repository;
using LeagueViewer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace LeagueViewer.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "LeagueViewerAPI", Version = "v1" });


            });
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicity",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            var connection = @"Data Source=.\sqlexpress; Initial Catalog=LeagueViewerDB; Integrated Security=True;Max Pool Size=500";
            services.AddDbContext<LeagueViewerContext>(options => options.UseSqlServer(connection));

            services.AddTransient<IRepository<League>, GenericRepository<League>>();
            services.AddTransient<IRepository<Team>, GenericRepository<Team>>();
            services.AddTransient<IRepository<Player>, GenericRepository<Player>>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ILeagueService, LeagueService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IPlayerService, PlayerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LeagueViewerAPI V1");
            });
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
