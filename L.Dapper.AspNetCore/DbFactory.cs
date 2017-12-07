using System.Data;

namespace L.Dapper.AspNetCore
{
    public class DbFactory
    {
        private readonly Dapper _dapper;

        public DbFactory(Dapper dapper)
        {
            _dapper = dapper;
        }

        public IDbConnection GetDbInstance(Dapper dapper=null)
        {
            //获取配置信息
            var config = _dapper.Config;
            if (dapper!=null)
            {
                config = dapper.Config;
            }
            IDbConnection db = null;
            switch (config.DbType)
            {
                case DbType.MSSQLServer:
                    db = MSSQLServer.GetDbInstance(config.ConnectionString);
                    break;
                default:
                    db = MSSQLServer.GetDbInstance(config.ConnectionString);
                    break;
            }
            return db;
        }
    }
}