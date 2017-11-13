using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace L.PathogenServices.Entities
{
    public class NovelType
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 小说
        /// </summary>
        public ICollection<Novel> Novels { get; set; }
    }
}