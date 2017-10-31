using Microsoft.Owin.Hosting;
using System;
using Topshelf;

namespace Hangfire.Topshelf.Infrastructure
{
    public class HangfireTopshelfService : ServiceControl
    {
        /// <summary>
        /// hangfire地址
        /// </summary>
        public string Address { get; set; }

        private IDisposable webApp;

        /// <summary>
        ///
        /// </summary>
        /// <param name="hostControl"></param>
        /// <returns></returns>
        public bool Start(HostControl hostControl)
        {
            try
            {
                webApp = WebApp.Start<Startup>(Address);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hostControl"></param>
        /// <returns></returns>
        public bool Stop(HostControl hostControl)
        {
            try
            {
                webApp?.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}