using System;
using System.Collections.Generic;
using System.Text;

namespace L.PathogenServices.Dto
{
    /// <summary>
    /// 获取储值表数据条件
    /// </summary>
    public class GetUserNewsInput
    {
        /// <summary>
        /// 会员账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 储值次数
        /// </summary>
        public int? StoreTimes { get; set; }

        /// <summary>
        /// 是否储值
        /// </summary>
        public bool? IsDeposit { get; set; }
        /// <summary>
        /// 会员状态
        /// </summary>
        public int? IsShow { get; set; }

        /// <summary>
        /// 起始存款日期
        /// </summary>
        public DateTime? BillStart { get; set; }
        /// <summary>
        /// 结束存款日期
        /// </summary>
        public DateTime? BillEnd { get; set; }
        private bool isStoreDate;
        /// <summary>
        /// 是否有存款日期
        /// </summary>
        public bool IsStoreDate
        {
            get
            {
                if (BillStart.HasValue || BillEnd.HasValue)
                {
                    return true;
                }
                return false;
            }
            set
            {
                isStoreDate = value;
            }
        }
    }
}
