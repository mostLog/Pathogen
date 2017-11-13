using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace L.PathogenServices.Entities
{
    /// <summary>
    /// 小说
    /// </summary>
    [Table("T_Novel")]
    public class Novel
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 小说名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// url地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 是否启动邮件推送
        /// </summary>
        public bool IsOpenEmail { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 是否已经爬取文章
        /// </summary>
        public bool IsCrawlerArticle { get; set; }

        /// <summary>
        /// 是否启动周期性任务
        /// </summary>
        public bool IsRecurrent { get; set; }

        /// <summary>
        /// 文章集合
        /// </summary>
        [Computed]
        public virtual ICollection<Article> Articles { get; set; }

        /// <summary>
        /// 小说类别
        /// </summary>
        public virtual NovelType NovelType { get; set; }
    }
}