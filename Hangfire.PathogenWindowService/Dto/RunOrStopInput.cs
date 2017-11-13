using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hangfire.PathogenWindowService.Dto
{
    /// <summary>
    /// 启动或者停止
    /// </summary>
    public class RunOrStopInput
    {
        /// <summary>
        /// 是否开启循环任务
        /// </summary>
        public bool IsRecurrent { get; set; }

        /// <summary>
        /// pathogenId
        /// </summary>
        [Required(ErrorMessage ="pathogenid不能为空！")]
        public string PathogenId { get; set; }
    }
}
