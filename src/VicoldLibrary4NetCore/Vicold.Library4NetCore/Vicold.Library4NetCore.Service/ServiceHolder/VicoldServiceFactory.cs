using System;
using System.Collections.Generic;
using System.Text;

namespace Vicold.Library4NetCore.Service.ServiceHolder
{
    /// <summary>
    /// 服务工厂
    /// </summary>
    public class VicoldServiceFactory
    {
        /// <summary>
        /// 创建服务池
        /// </summary>
        /// <returns></returns>
        public IVicoldService Create() => new VicoldService();
    }
}
