using Dapper.Contrib.Extensions;
using System;

namespace L.PathogenServices.Entities
{
    /// <summary>
    /// 文章
    /// </summary>
    [Table("T_Article")]
    public class Article
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否已爬取文章内容
        /// </summary>
        public bool IsCrawlerContent { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public long Seq { get; set; }

        /// <summary>
        /// 小说id
        /// </summary>
        public int NovelId { get; set; }

        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime OperaterDateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 小说
        /// </summary>
        [Computed]
        public virtual Novel Novel { get; set; }

    }
}