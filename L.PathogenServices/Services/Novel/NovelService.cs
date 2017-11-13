using Dapper;
using L.Dapper.AspNetCore;
using L.Dapper.AspNetCore.Extension;
using L.PathogenServices.Dto;
using L.PathogenServices.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace L.Application.Services
{
    public class NovelService : INovelService
    {
        //数据库工厂
        private readonly DbFactory _factory;
        public NovelService(DbFactory factory) 
        {
            _factory = factory;
        }

        public void AddNovel(Novel input)
        {
            using (IDbConnection db = _factory.GetDbInstance())
            {
                db.Insert(input);
            }
        }

        /// <summary>
        /// 更新小说信息
        /// </summary>
        public void UpdateNovel(Novel input)
        {
            using (IDbConnection db=_factory.GetDbInstance())
            {
                db.Update(input);
            }
        }

        /// <summary>
        /// 获取小说
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Novel GetSingleNovel(NovelSearchInput input)
        {
            string whereSql = string.Empty;
            var parameters=new DynamicParameters();
            //小说名
            if (!string.IsNullOrEmpty(input.Name))
            {
                whereSql += " where Name like @Name";
                parameters.Add("Name","%"+input.Name+"%");
            }
            using (IDbConnection db=_factory.GetDbInstance())
            {
                string sql = "select * from t_novel "+whereSql;
                return db.QueryList<Novel>(sql+whereSql, parameters).FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取小说列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IList<Novel> GetNovels(NovelSearchInput input)
        {
            string sql = "select * from t_novel where IsCrawlerArticle=@IsCrawlerArticle";
            var parameters = new DynamicParameters();
            StringBuilder whereSql = new StringBuilder();
            //小说名
            if (!string.IsNullOrEmpty(input.Name))
            {
                whereSql.Append(" and Name like (@Name)");
                parameters.Add("IsCrawlerArticle","%"+input.Name+"%");
            }
            parameters.Add("IsCrawlerArticle",input.IsCrawlerArticle);
            using (IDbConnection db=_factory.GetDbInstance())
            {
                return db.QueryList<Novel>(sql+whereSql,parameters).ToList();
            }
        }

        #region 文章操作方法
        /// <summary>
        /// 获取小说列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IList<ArticleListOutput> GetArticlesByNovelId(BaseDto input)
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<Article, ArticleListOutput>());
            if (input.Id.HasValue)
            {
                using (IDbConnection db=_factory.GetDbInstance())
                {
                    var articlesSql = "select * from t_article where NovelId=@NovelId order by seq";
                    //查询对应文章信息
                    var articles=db.QueryList<Article>(articlesSql,new { NovelId=input.Id.Value }).ToList();
                    if (articles != null&&articles.Count>0)
                    {
                        return AutoMapper.Mapper.Map<IList<ArticleListOutput>>(articles);
                    }
                }
            }
            return new List<ArticleListOutput>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Article GetArticleById(int id)
        {
            using (IDbConnection db=_factory.GetDbInstance())
            {
                return db.Get<Article>(id);
            }
        }

        /// <summary>
        /// 获取所有未爬取章节内容的文章
        /// </summary>
        /// <returns></returns>
        public IList<Article> GetArticles(ArticleSearchInput input)
        {
            string sql = string.Format("select top {0} * from t_article where IsCrawlerContent=@IsCrawlerContent", input.RowCount);

            StringBuilder whereSql = new StringBuilder();
            var parameters = new DynamicParameters();

            if (input.Seq != null)
            {
                whereSql.Append(" and Seq=@Seq");
                parameters.Add("Seq", input.Seq);
            }
            parameters.Add("IsCrawlerContent", input.IsCrawlerContent);
            using (IDbConnection db = _factory.GetDbInstance())
            {
                var articles = db.QueryList<Article>(sql+whereSql.ToString(), parameters);
                //小说id集合
                var novelIds = articles.Select(m => m.NovelId).Distinct();
                if (novelIds.Count() > 0)
                {
                    string novelSql = string.Format("select * from t_novel where novelid in({0})", string.Join(",", novelIds));
                    //获取小说集合
                    var novels = db.QueryList<Novel>(novelSql, new { });
                    foreach (var article in articles)
                    {
                        article.Novel = novels.FirstOrDefault(m => m.Id == article.NovelId);
                    }
                }
                return articles.ToList();
            }
        }

        /// <summary>
        /// 获取数据库最新小说
        /// </summary>
        /// <returns></returns>
        public Article GetLaestArticle()
        {
            string sql = "select * from t_article order by seq desc";
            using (IDbConnection db=_factory.GetDbInstance())
            {
                return db.QuerySingleOrDefault<Article>(sql,new { });
            }
        }

        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="article"></param>
        public void AddArticles(IList<Article> articles)
        {
            using (IDbConnection db=_factory.GetDbInstance())
            {
                db.BatchInsert(articles);
            }
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="article"></param>
        public void UpdateArticel(Article article)
        {
            using (IDbConnection db=_factory.GetDbInstance())
            {
                db.Update(article);
            }
        } 
        #endregion
    }
}