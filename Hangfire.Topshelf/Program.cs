using Hangfire.Topshelf.Infrastructure.Extension;
using System;
using Topshelf;

namespace Hangfire.Topshelf
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            return (int)HostFactory.Run(c =>
            {
                c.RunAsLocalSystem();
                //服务名称
                c.SetServiceName("Hangfire Service");
                //展示名称
                c.SetDisplayName("数据爬取服务");
                //描述信息
                c.SetDescription("数据爬取服务");

                c.UseOwin(AppSettings.Instance.ServiceAddress);
                //
                c.SetStartTimeout(TimeSpan.FromMinutes(1));

                c.SetStopTimeout(TimeSpan.FromMinutes(1));
            });
        }
    }
}