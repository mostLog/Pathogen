using L.PathogenServices.Dto;
using L.PathogenServices.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace L.Application.Services
{
    public interface INovelService
    {
       

        /// <summary>
        /// 获取小说
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Novel GetSingleNovel(NovelSearchInput input);

        /// <summary>
        /// 获取小说集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        IList<Novel> GetNovels(NovelSearchInput input);

        /// <summary>
        /// 添加小说
        /// </summary>
        /// <param name="input"></param>
        void AddNovel(Novel input);

        void UpdateNovel(Novel input);

        /// <summary>
        /// 获取小说章节列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        IList<ArticleListOutput> GetArticlesByNovelId(BaseDto input);

        /// <summary>
        /// 获取通过id获取章节
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Article GetArticleById(int id);

        /// <summary>
        /// 获取所有未爬取章节内容的文章
        /// </summary>
        /// <returns></returns>
        IList<Article> GetArticles(ArticleSearchInput input);

        /// <summary>
        /// 添加文章集合
        /// </summary>
        /// <param name="article"></param>
        void AddArticles(IList<Article> articles);

        /// <summary>
        ///
        /// </summary>
        /// <param name="article"></param>
        bool UpdateArticel(Article article);

        /// <summary>
        /// 获取数据库最新小说
        /// </summary>
        /// <returns></returns>
        Article GetLaestArticle();
    }
}