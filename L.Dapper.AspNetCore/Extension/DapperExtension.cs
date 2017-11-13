using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace L.Dapper.AspNetCore.Extension
{
    public static class DapperExtension
    {
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <returns></returns>
        public static long BatchInsert<T>(this IDbConnection db, IList<T> list)
        {
            try
            {
                if (list != null && list.Count > 0)
                {
                    var type = typeof(T);
                    //获取表名
                    string tableName = GetTableName(type);
                    var ps = type.GetType().GetProperties();
                    List<string> cols = new List<string>();
                    List<string> pars = new List<string>();
                    //构建插入字段
                    foreach (var p in ps)
                    {
                        //排除主键 和非数据库操作属性
                        if (!p.CustomAttributes.Any(x => x.AttributeType == typeof(KeyAttribute))
                            && !p.CustomAttributes.Any(x => x.AttributeType == typeof(ComputedAttribute)))
                        {
                            cols.Add(string.Format("[{0}]", p.Name));
                            pars.Add(string.Format("@{0}", p.Name));
                        }
                    }
                    //构建sql语句
                    var sql = string.Format(
                        "INSERT INTO [{0}] ({1}) VALUES({2})",
                        tableName,
                        string.Join(", ", cols),
                        string.Join(", ", pars));
                    return db.Execute(sql, list, null, null, null);
                }
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetTableName(Type type)
        {
            string name = string.Empty;
            var tableAttr = type
                .GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic;
            if (tableAttr != null)
            {
                name = tableAttr.Name;
            }
            else
            {
                name = type.Name + "s";
                if (type.IsInterface && name.StartsWith("I"))
                {
                    name = name.Substring(1);
                }
            }
            return name;
        }
    }
}
