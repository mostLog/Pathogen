using System;
using System.Collections.Generic;
using System.Text;

namespace L.PathogenServices.Dto
{
    public class UserDataInput
    {
        public GetMemberInput Member { get; set; }

        public GetUserNewsInput UserNews { get; set; }
     
        /// <summary>
        /// 当前第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 加入日期排序方式
        /// </summary>
        public string OrderJoin { get; set; }
    }
}
