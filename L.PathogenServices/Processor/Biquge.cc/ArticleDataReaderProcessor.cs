using L.Application.Services;
using L.LCore.Infrastructure.Dependeny;
using L.PathogenCore;
using L.PathogenServices.Dto;
using System.Collections.Generic;
using System.Linq;

namespace L.PathogenServices.Processor.Biquge.cc
{
    public class ArticleDataReaderProcessor : IDataReaderProcessor
    {
        private INovelService _novelService = ContainerManager.Resolve<INovelService>();
        /// <summary>
        /// 读取数据
        /// </summary>
        public IList<InfectionTarget> Reader()
        {
            return _novelService.GetArticles(
                new ArticleSearchInput()
                {
                    IsCrawlerContent = false,
                    RowCount = 10
                }).Select(m =>
                {
                    return new InfectionTarget()
                    {
                        Url = m.Url,
                        ExtraObj = m
                    };
                }).ToList();
        }
    }
}
