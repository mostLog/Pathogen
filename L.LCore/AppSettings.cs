using Microsoft.Extensions.Configuration;
using System;

namespace L.LCore
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
        /// 
        /// </summary>
        public string AccountServiceAddress => Configuration.GetConnectionString("Account");
        /// <summary>
        /// 
        /// </summary>
        public string WEBDBServiceAddress => Configuration.GetConnectionString("WEBDB");
        /// <summary>
        /// 
        /// </summary>
        public string WEBHistoryDBServiceAddress => Configuration.GetConnectionString("WEBHistoryDB");


    }
}