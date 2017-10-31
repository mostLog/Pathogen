using System;
using Topshelf;
using Topshelf.HostConfigurators;

namespace Hangfire.Topshelf.Infrastructure.Extension
{
    /// <summary>
    /// topshelf 配置扩展
    /// </summary>
    public static class HostConfiguratorExtension
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="configurator"></param>
        /// <param name="baseAddress"></param>
        /// <returns></returns>
        public static HostConfigurator UseOwin(this HostConfigurator configurator, string baseAddress)
        {
            if (string.IsNullOrEmpty(baseAddress))
            {
                throw new ArgumentNullException(nameof(baseAddress));
            }
            //配置hangfire web服务
            configurator.Service<HangfireTopshelfService>(s =>
            {
                //
                s.ConstructUsing(() => new HangfireTopshelfService() { Address = baseAddress });
                //
                s.WhenStarted((service, control) => service.Start(control));
                //
                s.WhenStopped((service, control) => service.Stop(control));
            });

            return configurator;
        }
    }
}