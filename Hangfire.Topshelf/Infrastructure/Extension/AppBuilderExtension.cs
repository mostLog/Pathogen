using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Owin;
using System;

namespace Hangfire.Topshelf.Infrastructure.Extension
{
    public static class AppBuilderExtension
    {
        /// <summary>
        /// hangfire 界面仪表盘度量
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IAppBuilder UseDashboardMetric(this IAppBuilder app)
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
            return app;
        }

        /// <summary>
        /// 配置hangfire数据库地址
        /// </summary>
        /// <typeparam name="TStorage"></typeparam>
        /// <param name="app"></param>
        /// <param name="storage"></param>
        /// <returns></returns>
        public static IAppBuilder UseStorage<TStorage>(this IAppBuilder app, TStorage storage) where TStorage : JobStorage
        {
            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }
            GlobalConfiguration.Configuration.UseStorage(storage);
            return app;
        }


    }
}