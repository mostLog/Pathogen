using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using L.PathogenServices.Dto;
using L.Dapper.AspNetCore;
using L.PathogenCore;
using L.PathogenServices.Processor.Biquge.cc;

namespace L.PathogenServices.Services
{
    public class PathogenService : IPathogenService
    {
        private readonly DbFactory _factory;
        public PathogenService(DbFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// 根据PathogenId获取实体
        /// </summary>
        /// <param name="pathogenId"></param>
        /// <returns></returns>
        public PathogenOuput GetPathogenById(string pathogenId)
        {
            string sql = "select * from T_SpiderTask where SpiderId=@SpiderId";
            using (var db=_factory.GetDbInstance())
            {
                return db.QueryList<PathogenOuput>(sql,
                    new {
                        SpiderId =pathogenId
                    }).FirstOrDefault();
            }
        }

        /// <summary>
        /// 更新Pathogen状态
        /// </summary>
        public void UpdatePathogenStatus(string pathogenId, bool isRun)
        {
            string sql = "update T_SpiderTask set IsRecurrent=@IsRecurrent where SpiderId=@SpiderId";
            using (var db = _factory.GetDbInstance())
            {
                db.ExcuteSql(sql,new { IsRecurrent =isRun, SpiderId = pathogenId });
            }
        }

        /// <summary>
        /// 启动或者停止pathogen
        /// </summary>
        /// <param name="pathogenId"></param>
        /// <returns></returns>
        public void RunOrStopPathogen(string pathogenId)
        {
            IPathogen pathogen=null;
            switch (pathogenId)
            {
                case "ArticlePathogen":
                    pathogen = new DataPathogen(new ArticleProcessor(),new ArticleDataReaderProcessor());
                    
                    break;
                default:
                    break;
            }
            if (pathogen!=null) pathogen.Infected();
        }
    }
}
