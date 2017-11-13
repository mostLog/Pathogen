using L.Application.Services;
using L.LCore.Email;
using L.LCore.Infrastructure.Dependeny;
using L.PathogenCore;
using L.PathogenServices.Entities;
using System;
using System.Collections.Generic;

namespace L.PathogenServices.Processor.Biquge.cc
{
    public class ArticleProcessor : IProcessor
    {
        private INovelService _novelService = ContainerManager.Resolve<INovelService>();
        public ArticleProcessor()
        {

        }

        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="resultDatas"></param>
        public void DataProcess(IDictionary<string, object> resultDatas)
        {
            string url = resultDatas["requestUrl"] as string;
            try
            {
                string content = resultDatas["article"] as string;
                //获取文章对象
                Article article = resultDatas["extraObj"] as Article;
                if (!string.IsNullOrEmpty(content))
                {
                    article.Content = content;
                }
                article.IsCrawlerContent = true;
                //更新信息
                _novelService.UpdateArticel(article);
                //是否启动邮件发送
                if (article.Novel != null && article.Novel.IsOpenEmail)
                {
                    //发送邮件
                    EmailHelper.SendEmail(article.Title, content, new List<string>() { "2434934089@qq.com" });
                }
            }
            catch (Exception e)
            {
               
            }
           
        }

        /// <summary>
        /// 页面解析
        /// </summary>
        /// <param name="pagePathogen"></param>
        public void PageProcess(PagePathogen pagePathogen)
        {
            try
            {
                //添加请求地址
                pagePathogen.AddResult("requestUrl",pagePathogen.Url);
                var selector = new XPathSelector(pagePathogen.PageSource);
                var node = selector.SelectSingleNode("//*[@id='content']");
                if (node != null)
                {
                    pagePathogen.AddResult("article", node.InnerHtml);
                }
                else
                {
                   
                }
            }
            catch (Exception e)
            {
               
            }
        }

       
    }
}
