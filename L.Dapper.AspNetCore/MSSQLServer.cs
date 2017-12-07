using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace L.Dapper.AspNetCore
{
    /// <summary>
    /// sql server
    /// </summary>
    public static class MSSQLServer
    {
        /// <summary>
        ///
        /// </summary>
        public static IDbConnection GetDbInstance(string connections)
        {
            IDbConnection db = new SqlConnection(connections);
            db.Open();
            return db;
        }

        /// <summary>
        /// 添加
        /// </summary>
        public static long InsertEntity<T>(this IDbConnection db,T t) where T : class
        {
            try
            {
                CheckDbState(db);
                return db.Insert(t);
            }
            catch (System.Exception)
            {

                throw;
            }
    
        }

        

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public static bool DeleteEntity<T>(this IDbConnection db,T t) where T:class
        {
            try
            {
                CheckDbState(db);
                return db.Delete(t);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool UpdateEntity<T>(this IDbConnection db,T t) where T:class
        {
            try
            {
                CheckDbState(db);
                return db.Update(t);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetEntity<T>(this IDbConnection db,int id) where T:class
        {
            try
            {
                CheckDbState(db);
                return db.Get<T>(id);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IEnumerable<T> QueryList<T>(this IDbConnection db, string sql, object param)
        {
            CheckDbState(db);
            return db.Query<T>(sql, param);
        }
        /// <summary>
        /// 多条sql查询
        /// </summary>
        /// <returns></returns>
        public static SqlMapper.GridReader QueryMul(this IDbConnection db,string sql,object param)
        {
            try
            {
                CheckDbState(db);
                return db.QueryMultiple(sql, param);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QueryScalar<T>(this IDbConnection db, string sql, object param = null)
        {
            CheckDbState(db);
            if (param == null)
            {
                return db.ExecuteScalar<T>(sql);
            }
            else
            {
                return db.ExecuteScalar<T>(sql, param);
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int ExcuteSql<T>(this IDbConnection db, string sql, T p)
        {
            CheckDbState(db);
            return db.Execute(sql, p);
        }


        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static async Task<int> ExcuteSqlAsync<T>(this IDbConnection db, string sql, T p)
        {
            CheckDbState(db);
            return await db.ExecuteAsync(sql, p);
        }

        /// <summary>
        /// 检测数据库状态
        /// </summary>
        private static void CheckDbState(IDbConnection db)
        {
            //如果连接已关闭
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
            }
        }
    }
}