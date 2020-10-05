using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Writers;
using Newtonsoft.Json;
using Petshop2020.Core.Application_Service;
using Petshop2020.Core.Application_Service.Service;
using Petshop2020.Core.Domain_Service;
using Petshop2020.Infrastructure.SQLite.Data;
using Petshop2020.Infrastructure.SQLite.Data.Repositories;

namespace Petshop2020.WebApi
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
            //This is used for in memory
            /*
            services.AddDbContext<PetshopContext>
                (
                    opt => opt.UseInMemoryDatabase("TheDB")
                );
            */

            //----- DATABASE -----
            services.AddDbContext<PetshopContext>
                (
                    opt => opt.UseSqlite("Data Source=petshop.db").EnableSensitiveDataLogging(), ServiceLifetime.Transient
                );

            //----- AUTHENTICATION -----
            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    //ValidAudience = "TodoApiClient",
                    ValidateIssuer = false,
                    //ValidIssuer = "TodoApi",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });


            //----- SWAGGER -----
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Swagger Demo API",
                        Description = "Swag for petshop",
                    });

            });

            //----- SECURITY -----
            services.AddCors(options =>
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    })
                );


            // Avoiding reference loops
            services.AddControllers().AddNewtonsoftJson
                (x => x.SerializerSettings.ReferenceLoopHandling = 
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IPetTypeRepository, PetTypeRepository>();
            services.AddScoped<IPetTypeService, PetTypeService>();
            services.AddControllers();

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<PetshopContext>();
                    DBInitializer.SeedDB(ctx);
                }
            }

            /* Initialize data for old fake db / static data
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var repo = scope.ServiceProvider.GetService<IPetRepository>();
                var repoOwner = scope.ServiceProvider.GetService<IOwnerRepository>();
                var type = scope.ServiceProvider.GetService<IPetTypeRepository>();
                new DataInitializer(repo, repoOwner, type).InitData();
            }
            */


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseCors();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo");
            });
        }
    }
}
