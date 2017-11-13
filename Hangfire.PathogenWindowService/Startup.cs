using L.LCore.Infrastructure.Extension;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace Hangfire.PathogenWindowService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //配置swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info()
                {
                    Version = "v1",
                    Title = "Crawler API"
                });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Hangfire.PathogenWindowService.xml");
                options.IncludeXmlComments(xmlPath);
            });
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication("pathogen",options =>
            {
                options.Authority = "http://localhost:8889";
                options.RequireHttpsMetadata = false;
                options.ApiName = "api1";
            });
            return services.ConfigureApplicationServices(Configuration);
        }

        /// <summary>
        /// 中间件配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //添加授权
            app.UseAuthentication();
            app.UseMvc();
            //添加swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crawler API V1");
            });
            app.ConfigureRequestMiddleware();
        }
    }
}