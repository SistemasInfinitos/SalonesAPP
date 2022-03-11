using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SalonesAPI.ModelsDB;
using System;

namespace SalonesAPI
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
            string[] audience = Configuration["JwtConfig:Audience"].ToString().Split(",");
            string connectionString = Configuration["JwtConfig:connectionString"];
            
            //services.AddCors();
            services.AddCors(options =>
            {
                //options.AddPolicy(name: "AudienciaPolicy", builder => { builder.WithOrigins(audience).AllowAnyHeader().AllowAnyMethod(); });//produccion
                options.AddPolicy(name: "AudienciaPolicy", builder => { builder.SetIsOriginAllowed(origen => new Uri(origen).Host == "localhost").AllowAnyHeader().AllowAnyMethod(); });
                
            });
            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SalonesAPI", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //string[] audience = Configuration["JwtConfig:Audience"].ToString().Split(",");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SalonesAPI v1"));
            }
            //app.UseCors(options => options.WithOrigins(audience).AllowAnyMethod().AllowAnyHeader());
            app.UseCors("AudienciaPolicy");
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
