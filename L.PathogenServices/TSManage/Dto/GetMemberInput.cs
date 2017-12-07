using System;
using System.Collections.Generic;
using System.Text;

namespace L.PathogenServices.Dto
{
    /// <summary>
    /// 获取会员表信息查询条件
    /// </summary>
    public class GetMemberInput
    {
        /// <summary>
        /// 会员帐号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///代理帐号
        /// </summary>
        public string Algent { get; set; }
        /// <summary>
        /// 未登入天数
        /// </summary>
        public int? NoEnterDay { get; set; }
        /// <summary>
        /// 会员等级(0:尊龙级 1:金级 2:白金级 3:钻石级 4:大神级)
        /// </summary>
        public int? Level { get; set; }
        /// <summary>
        /// 起始加入日期
        /// </summary>
        public DateTime? JoinStart { get; set; }
        /// <summary>
        /// 结束加入日期
        /// </summary>
        public DateTime? JoinEnd { get; set; }
    }
}
