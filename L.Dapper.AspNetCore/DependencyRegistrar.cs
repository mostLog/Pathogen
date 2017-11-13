using Autofac;
using L.LCore.Infrastructure.Dependeny;

namespace L.Dapper.AspNetCore
{
    /// <summary>
    /// dapper层依赖注入配置
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder)
        {
            //初始化数据库配置
            builder.Register(c => new Dapper(new DapperConfig()
            {
                ConnectionString = "Max Pool Size=512;;data source=.;initial catalog=CoreTest;uid=sa;pwd=sa;",
                DbType = DbType.MSSQLServer
            })).SingleInstance();

            builder.RegisterType<DbFactory>()
                .InstancePerLifetimeScope();
        }

        public int Order { get; set; } = 2;
    }
}