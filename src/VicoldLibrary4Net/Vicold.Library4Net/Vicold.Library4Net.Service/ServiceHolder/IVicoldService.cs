using System;

namespace Vicold.Library4Net.Service.ServiceHolder
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
        T  UnRegister<T>( string key = "default") where T : class;
        /// <summary>
        /// 获取已注册服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetService<T>(string key = "default") where T : class;
    }
}
