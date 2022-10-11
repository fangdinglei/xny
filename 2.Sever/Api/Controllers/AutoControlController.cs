using Microsoft.AspNetCore.Mvc;

namespace XNYAPI.Controllers
{
    //TODO
    public class AutoControlController:Controller
    {
        /// <summary>
        /// 获取自动控制状态
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetState(string name="all") {
            return null;
        }

        /// <summary>
        /// 获取自动控制状态详细信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetInfo(string name = "all")
        {
            return null;
        }

        /// <summary>
        /// 设置自动控制状态
        /// </summary>
        /// <param name="name"></param>
        /// <param name="open"></param>
        /// <returns></returns>
        public string SetState(string name = "all",bool open =true ) {
            return null;
        }

        /// <summary>
        /// 获取自动控制所产生的动作指令
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetActions( string ids,string name = "all") {
            return null;
        }

    }
}
