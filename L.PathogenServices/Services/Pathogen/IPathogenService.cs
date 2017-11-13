using L.PathogenServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace L.PathogenServices.Services
{
    public interface IPathogenService
    {
        /// <summary>
        /// 根据PathogenId获取实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PathogenOuput GetPathogenById(string pathogenId);

        /// <summary>
        /// 启动或关闭pathogen
        /// </summary>
        /// <returns></returns>
        void RunOrStopPathogen(string pathogenId);
    }
}
