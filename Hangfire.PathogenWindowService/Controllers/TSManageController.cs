using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using L.PathogenServices.Dto;
using Hangfire.PathogenWindowService.Dto;

namespace Hangfire.PathogenWindowService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class TSManageController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult GetPagedData(UserDataInput input)
        {
            //检验参数
            var valide=ValidateParameters(input);
            if (!valide.IsOk)
            {
                return Json(new {
                    status=0,
                    message=valide.Message
                });
            }
            //获取数据

            return Json(11);
        }

        /// <summary>
        /// 验证参数
        /// </summary>
        private ValidatableObj ValidateParameters(UserDataInput input)
        {
            //选择有存款 但存款次数填写为0
            if (input.StoreTimes.HasValue&&
                input.StoreTimes.Value == 0&&
                input.IsDeposit.HasValue&&
                input.IsDeposit.Value
                )
            {
                return new ResponseMessage() {
                    Status=Status.Fail,
                    Message="非法操作"
                };
            }
            //加入日期
            if (input.JoinStart.HasValue&& input.JoinEnd.HasValue)
            {
                DateTime joinStart = input.JoinStart.Value;
                DateTime joinEnd = input.JoinEnd.Value;
                if (joinStart > joinEnd)
                {
                    return new ValidatableObj()
                    {
                        IsOk=false,
                        Message="非法操作"
                    };
                }
            }
            //存款日期
            if (input.BillStart.HasValue&& input.BillEnd.HasValue)
            {
                DateTime billStart = input.BillStart.Value;
                DateTime billEnd = input.BillEnd.Value;
                if (billStart>billEnd)
                {
                    return new ValidatableObj()
                    {
                        IsOk=false,
                        Message = "非法操作"
                    };
                }
            }
            //敏感词校验
            return new ValidatableObj() {
                IsOk=true
            };
        }
    }
}
