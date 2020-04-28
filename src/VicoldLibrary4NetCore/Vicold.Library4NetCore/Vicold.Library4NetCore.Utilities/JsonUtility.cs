using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Vicold.Library4NetCore.Utilities
{
    /// <summary>
    /// Json（文件）与实体类的映射
    /// </summary>
    public static class JsonUtility
    {
        /// <summary>
        /// Json映射到实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToEntityFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 实体类映射到Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string ToJsonFromEntity<T>(T entity)
        {
            return JsonConvert.SerializeObject(entity);
        }

        /// <summary>
        /// Json文件映射到实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonPath"></param>
        /// <returns></returns>
        public static Task<T> ToEntityFromJsonFileAsync<T>(string jsonPath)
        {
            var tcs = new TaskCompletionSource<T>();
            try
            {
                if (!File.Exists(jsonPath))
                {
                    var ex = new FileNotFoundException("XML文件未找到");
                    tcs.SetException(ex);
                }
                else
                {
                    var json = File.ReadAllText(jsonPath);
                    var result = JsonConvert.DeserializeObject<T>(json);
                    tcs.SetResult(result);
                }
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
            return tcs.Task;
        }

        /// <summary>
        /// 实体类映射到Json文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonPath"></param>
        /// <returns></returns>
        public static Task ToJsonFileFromEntityAsync<T>(T entity, string jsonPath)
        {
            var tcs = new TaskCompletionSource<T>();
            try
            {
                var directory = Path.GetDirectoryName(jsonPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                var result = JsonConvert.SerializeObject(entity);
                File.WriteAllText(jsonPath, result);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
                return tcs.Task;
            }
        }
    }
}
