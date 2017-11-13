using Hangfire.PathogenWindowService.Dto;
using L.PathogenServices.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Hangfire.PathogenWindowService.Controllers
{
    /// <summary>
    /// pathogen服务接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class PathogenController: Controller
    {

        private readonly IPathogenService _pathogenService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathogenService"></param>
        public PathogenController(IPathogenService pathogenService)
        {
            _pathogenService = pathogenService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RunOrStopPathogen(RunOrStopInput input)
        {
            var pathogen = _pathogenService.GetPathogenById(input.PathogenId);
            if (pathogen != null)
            {
                //开启循环
                if (input.IsRecurrent)
                {
                    RecurringJob.AddOrUpdate<IPathogenService>(
                        input.PathogenId,
                        a => a.RunOrStopPathogen(input.PathogenId),
                        pathogen.RecurrentCron,
                        TimeZoneInfo.Local);
                }
                else
                {
                    RecurringJob.AddOrUpdate<IPathogenService>(
                        input.PathogenId,
                        a => a.RunOrStopPathogen(input.PathogenId),
                        pathogen.RecurrentCron,
                        TimeZoneInfo.Local);
                }
            }
            else
            {
                return Json("pathogen不存在！");
            }
            return Json(1);
        }
    }
}
