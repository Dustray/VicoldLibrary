//==================================================================== 
//**   解决方案或项目总名称
//====================================================================
//**   Copyright © 中国气象局 2020 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：IVicoldService.cs
// 项目名称：请更改为实际项目名称
// 创建时间：2020/3/17 14:59:02
// 创建人员：宋杰军
// 负 责 人：宋杰军
// 参与人员：宋杰军
// ===================================================================
// 修改日期：
// 修改人员：
// 修改内容：
// ===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vicold.Library4NetCore.Service.ServiceHolder
{
    /// <summary>
    /// 服务池接口
    /// </summary>
    public interface IVicoldService : IDisposable
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="key"></param>
        void Register<T>(T t, string key = "default") where T : class;
        /// <summary>
        /// 反注册服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T UnRegister<T>(string key = "default") where T : class;
        /// <summary>
        /// 获取已注册服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetService<T>(string key = "default") where T : class;
    }

}
