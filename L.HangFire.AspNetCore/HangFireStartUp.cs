using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using L.LCore.Infrastructure.Dependeny;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace L.HangFire
{
    public class HangFireStartUp : IStartUp
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public void ConfigureServices(IServiceCollection services)
        {
            //添加HangFire
            services.AddHangfire(h => h.UseSqlServerStorage("data source=.;initial catalog=CoreTestHangFire;uid=sa;pwd=sa;", new Hangfire.SqlServer.SqlServerStorageOptions()
            {
                PrepareSchemaIfNecessary = true
            }));
        }

        /// <summary>
        /// 配置请求中间件
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            UseDashboardMetric();
        }

        /// <summary>
        /// hangfire 界面仪表盘度量
        /// </summary>
        private void UseDashboardMetric()
        {
            GlobalConfiguration.Configuration
                .UseDashboardMetric(DashboardMetrics.ServerCount)
                .UseDashboardMetric(SqlServerStorage.ActiveConnections)
                .UseDashboardMetric(SqlServerStorage.TotalConnections)
                .UseDashboardMetric(DashboardMetrics.RecurringJobCount)
                .UseDashboardMetric(DashboardMetrics.RetriesCount)
                .UseDashboardMetric(DashboardMetrics.AwaitingCount)
                .UseDashboardMetric(DashboardMetrics.EnqueuedAndQueueCount)
                .UseDashboardMetric(DashboardMetrics.ScheduledCount)
                .UseDashboardMetric(DashboardMetrics.ProcessingCount)
                .UseDashboardMetric(DashboardMetrics.SucceededCount)
                .UseDashboardMetric(DashboardMetrics.FailedCount)
                .UseDashboardMetric(DashboardMetrics.DeletedCount);
        }
        /// <summary>
        /// 序号
        /// </summary>
        public int Order
        {
            get;
            set;
        } = 1;
    }
}