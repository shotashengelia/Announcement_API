using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using AnnouncementDatabase;
using AnnouncementDatabase.Context;
using Swashbuckle.AspNetCore.Swagger;
using Announcement_API.Model;

namespace Announcement_API
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
           var ConnectionString = Configuration["ConnectionString:AnouncementsConnectionString"];

            services.AddDbContext<AnnouncementDbContext>(options => options.UseSqlServer(ConnectionString,
            x => x.MigrationsAssembly("AnnouncementDatabase")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Core Api", Description = "Swagger Core Api" });
            
                });

            services.AddDbContext<AnnouncementDbContext>(options => options.UseSqlServer(ConnectionString,
               x => x.MigrationsAssembly("AnnouncementDatabase")));
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

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Announcement, Get_Announcement>();
                cfg.CreateMap<Announcement, Add_Announcement>();  
                cfg.CreateMap<Announcement, Update_Announcement>();
            }
            );


            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","Core Api");
            }
                );
        }

    }
}
