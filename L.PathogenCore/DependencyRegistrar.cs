using Autofac;
using L.LCore.Infrastructure.Dependeny;

namespace L.PathogenCore
{
    /// <summary>
    /// SpiderCore层依赖注入配置
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder)
        {
           
     
        }

        public int Order { get; set; } = 5;
    }
}