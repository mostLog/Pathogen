using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfire.PathogenWindowService.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidatableObj
    {
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 是否匹配成功
        /// </summary>
        public bool IsOk { get; set; }
    }
}
