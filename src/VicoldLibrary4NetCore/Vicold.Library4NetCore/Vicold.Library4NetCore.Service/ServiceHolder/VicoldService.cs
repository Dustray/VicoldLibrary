using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Vicold.Library4NetCore.Service.ServiceHolder
{
    internal sealed class VicoldService : IVicoldService
    {
        private ConcurrentDictionary<string, WeakReference<object>> _servicePool = new System.Collections.Concurrent.ConcurrentDictionary<string, WeakReference<object>>();

        /// <summary>
        /// 获取已注册服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetService<T>(string key = "default") where T : class
        {
            if (_servicePool.TryGetValue(key, out var value))
            {
                if (value.TryGetTarget(out object target))
                    return target as T;
            }
            return default;
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="key"></param>
        public void Register<T>(T t, string key = "default") where T : class => _servicePool[key] = new WeakReference<object>(t);

        /// <summary>
        /// 反注册服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T UnRegister<T>(string key = "default") where T : class
        {
            if (_servicePool.TryRemove(key, out var value))
            {
                if (value.TryGetTarget(out object target))
                    return target as T;
            }
            return default;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose() => _servicePool.Clear();

    }
}
