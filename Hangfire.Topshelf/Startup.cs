using Hangfire.SqlServer;
using Hangfire.Topshelf.Infrastructure.Extension;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Hangfire.Topshelf.Startup))]

namespace Hangfire.Topshelf
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //webapi配置
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);

            //配置数据库地址
            app.UseStorage(new SqlServerStorage(AppSettings.Instance.HangfireSqlserverConnectionString));
            //hangfire配置
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
            });
            //hangfire 页面配置
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                AppPath = AppSettings.Instance.AppWebSite,
            });
            //界面仪表盘度量
            app.UseDashboardMetric();
        }
    }
}