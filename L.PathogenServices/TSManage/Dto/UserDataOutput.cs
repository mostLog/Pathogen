using System;
using System.Collections.Generic;
using System.Text;

namespace L.PathogenServices.Dto
{
    public class UserDataOutput
    {
        /// <summary>
        ///代理帐号
        /// </summary>
        public string Alagent { get; set; }
        /// <summary>
        /// 会员帐号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 加入日期
        /// </summary>
        public string JoinDate { get; set; }
        /// <summary>
        /// 未登录天数
        /// </summary>
        public int NoEnterDay { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int IsShow { get; set; }
        /// <summary>
        /// 储值次数
        /// </summary>
        public int StoreTimes { get; set; }
        /// <summary>
        /// 总输赢
        /// </summary>
        public decimal SumWinLose { get; set; }
        /// <summary>
        /// 总储值
        /// </summary>
        public decimal StoreMoney { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
