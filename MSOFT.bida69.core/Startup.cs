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
using MSOFT.BL;
using MSOFT.BL.Interfaces;
using MSOFT.DL;
using MSOFT.DL.Interfaces;
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
            DataAccess.ConnectionString = connectionString;
            bida69Context.ConnectionString = connectionString;
            //Entity Framework  
            services.AddDbContext<bida69Context>(options => options.UseSqlServer(connectionString));

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
            services.AddScoped(typeof(IBaseBL<>), typeof(EntityBL<>));
            services.AddScoped<IBaseRepository, BaseDL>();

            services.AddScoped<IInventoryBL, InventoryBL>();
            services.AddScoped<IInventoryCategoryBL, InventoryCategoryBL>();
            services.AddScoped<IRefBL, RefBL>();
            services.AddScoped<IRefDetailBL, RefDetailBL>();
            services.AddScoped<IRefServiceBL, RefServiceBL>();
            services.AddScoped<IServiceBL, ServiceBL>();
            services.AddScoped<IUserBL, UserService>();

            services.AddScoped<IInventoryCategoryRepository, InventoryCategoryDL>();
            services.AddScoped<IInventoryRepository, InventoryDL>();
            services.AddScoped<IRefDetailRepository, RefDetailDL>();
            services.AddScoped<IRefRepository, RefDL>();
            services.AddScoped<IRefServiceRepository, RefServiceDL>();
            services.AddScoped<IServiceRepository, ServiceDL>();
            services.AddScoped<IUserRepository, UserDL>();
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
