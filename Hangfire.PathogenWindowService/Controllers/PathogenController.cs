using Hangfire.PathogenWindowService.Dto;
using Hangfire.PathogenWindowService.Extension;
using L.HangFire.AspNetCore.Services;
using L.PathogenServices.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire.PathogenWindowService.Controllers
{
    /// <summary>
    /// pathogen服务接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [EnableCors("AllowCor")]
    public class PathogenController: ControllerBase
    {

        private readonly IPathogenService _pathogenService;
        private readonly IHangFireService _hangFireService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathogenService"></param>
        public PathogenController(IPathogenService pathogenService,IHangFireService hangFireService)
        {
            _pathogenService = pathogenService;
            _hangFireService = hangFireService;
        }

        /// <summary>
        /// 启动或者关闭pathogen
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RunOrStopPathogen(RunOrStopInput input)
        {
            //参数合法性校验
            if (ModelState.IsValid)
            {
                var pathogen = _pathogenService.GetPathogenById(input.PathogenId);
                if (pathogen != null)
                {
                    //开启或更新循环
                    _hangFireService.AddRecurrentSchedule<IPathogenService>(
                            input.PathogenId,
                            a => a.RunOrStopPathogen(input.PathogenId),
                            pathogen.RecurrentCron);
                    //更新pathogen状态
                    _pathogenService.UpdatePathogenStatus(input.PathogenId,input.IsRecurrent);
                    return Ok("ok");
                }
                else
                {
                    return BadRequest("pathogen不存在！");
                }
            }
            return BadRequest(ModelState.GetModelStateError());
        }
    }
}
