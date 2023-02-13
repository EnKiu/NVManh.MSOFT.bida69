using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MSOFT.bida69.core.Helpers;
using MSOFT.bida69.core.Middleware;
using MSOFT.bida69.Services;
using MSOFT.Core.Interfaces;
using MSOFT.Core.Service;
using MSOFT.Infrastructure.DatabaseContext;
using MSOFT.Infrastructure.Interfaces;
using MSOFT.Infrastructure.Repository;
using Newtonsoft.Json.Serialization;

namespace MSOFT.bida69.core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add servicefs to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Common.Common.TimeZoneId = Configuration.GetSection("TimeZoneId").Value;
            services.AddCors();
            // Bổ sung thông tin kết nối với Database:
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //services.Add(new ServiceDescriptor(typeof(DatabaseContext), new DatabaseContext(Configuration.GetConnectionString("LadizoneConnection"))));
            //DataAccess.ConnectionString = connectionString;
            //bida69Context.ConnectionString = connectionString;
            //Entity Framework  
            //services.AddDbContext<bida69Context>(options => options.UseSqlServer(connectionString));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = "http://10.0.6.58:31079";
            //    //options.InstanceName = "master";
            //    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions() { };
            //});
            services.AddDistributedRedisCache(o =>
            {
                o.Configuration = "http://10.0.6.58:31079";
            });
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = System.Text.Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            // configure DI for application services
            //services.AddScoped(typeof(IBaseBL<>), typeof(EntityBL<>));
            services.AddScoped<IEntityService, EntityService>();
            services.AddScoped<IBaseRepository, ADORepository>();
            services.AddScoped<IDataContext, MSMySqlConnector>();

            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IInventoryCategoryService, InventoryCategoryService>();
            services.AddScoped<IRefService, RefService>();
            services.AddScoped<IRefDetailService, RefDetailService>();
            services.AddScoped<IRefServiceService, RefServiceService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IServiceRepository, ServiceRepository>();

            services.AddScoped<IInventoryCategoryRepository, InventoryCategoryRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IRefDetailRepository, RefDetailRepository>();
            services.AddScoped<IRefRepository, RefRepository>();
            services.AddScoped<IRefServiceRepository, RefServiceRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
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
                app.UseHsts();
            }

            DefaultFilesOptions options = new DefaultFilesOptions();
            //options.DefaultFileNames.Clear();
            //options.DefaultFileNames.Add("html/index.html");
            //app.UseDefaultFiles(options);

            app.UseStaticFiles();
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Admin}/{action=Service}");
                endpoints.MapControllers();
            });

        }
    }
}
