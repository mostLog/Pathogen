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
                if (resultDatas.Keys.Contains("article")&&resultDatas.Keys.Contains("extraObj"))
                {
                    string content = resultDatas["article"] as string;
                    if (!string.IsNullOrEmpty(content))
                    {
                        //获取文章对象
                        Article article = resultDatas["extraObj"] as Article;
                        article.Content = content;
                        article.IsCrawlerContent = true;
                        //更新信息
                        _novelService.UpdateArticel(article);
                        System.Diagnostics.Debug.WriteLine("url:" + url + "/n" + "content:" + content);
                        //是否启动邮件发送
                        if (article.Novel != null && article.Novel.IsOpenEmail)
                        {
                            //发送邮件
                            EmailHelper.SendEmail(article.Title, content, new List<string>() { "2434934089@qq.com" });
                        }
                    }
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
                if (!string.IsNullOrEmpty(pagePathogen.PageSource))
                {
                    var selector = new XPathSelector(pagePathogen.PageSource);
                    var node = selector.SelectSingleNode("//*[@id='content']");
                    pagePathogen.AddResult("article", node?.InnerHtml ?? string.Empty);
                }else
                {
                    pagePathogen.AddResult("article", string.Empty);
                }
            }
            catch (Exception e)
            {
               
            }
        }

       
    }
}
