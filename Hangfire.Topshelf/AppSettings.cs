using Microsoft.Extensions.Configuration;
using System;

namespace Hangfire.Topshelf
{
    public class AppSettings
    {
        private static readonly Lazy<AppSettings> _instance = new Lazy<AppSettings>(() => new AppSettings());

        public static AppSettings Instance => _instance.Value;

        public IConfigurationRoot Configuration { get; }

        private AppSettings()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        /// <summary>
		/// Windows 服务名称
		/// </summary>
		public string ServiceName => Configuration["hangfire.server.serviceName"];

        /// <summary>
        /// Windows 服务显示名称
        /// </summary>
        public string ServiceDisplayName => Configuration["hangfire.server.serviceDisplayName"];

        /// <summary>
        /// Windows 服务描述
        /// </summary>
        public string ServiceDescription => Configuration["hangfire.server.serviceDescription"];

        /// <summary>
        /// 站点服务地址
        /// </summary>
        public string ServiceAddress => Configuration["hangfire.server.serviceAddress"];

        /// <summary>
        /// 站点地址
        /// </summary>
        public string AppWebSite => Configuration["hangfire.server.website"];

        /// <summary>
		/// Hangfire sql server 数据库连接字符串
		/// </summary>
		public string HangfireSqlserverConnectionString => Configuration.GetConnectionString("hangfire.sqlserver");
    }
}