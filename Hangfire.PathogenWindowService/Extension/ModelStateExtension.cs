using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfire.PathogenWindowService.Extension
{
    /// <summary>
    /// ModelState扩展
    /// </summary>
    public static class ModelStateExtension
    {
        /// <summary>
        /// 获取ModelState错误信息
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <returns></returns>
        public static string GetModelStateError(this ModelStateDictionary modelStateDictionary)
        {
            if (modelStateDictionary.IsValid || !modelStateDictionary.Any()) return string.Empty;
            foreach (string key in modelStateDictionary.Keys)
            {
                var tempModelState = modelStateDictionary[key];
                if (tempModelState.Errors.Any())
                {
                    var firstOrDefault = tempModelState.Errors.FirstOrDefault();
                    if (firstOrDefault != null) return firstOrDefault.ErrorMessage;
                }
            }
            return string.Empty;
        }
    }
}
