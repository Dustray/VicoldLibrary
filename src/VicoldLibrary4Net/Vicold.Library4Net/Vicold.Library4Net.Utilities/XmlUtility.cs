using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Vicold.Library4Net.Utilities
{
    /// <summary>
    /// XML与实体、JSON的映射类
    /// </summary>
    public static class XmlUtility
    {
        /// <summary>
        /// 从XML文件映射为实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public static Task<T> LoadXMLFileToEntityAsync<T>(string xmlPath)
        {
            var tcs = new TaskCompletionSource<T>();
            try
            {
                if (!File.Exists(xmlPath))
                {
                    var ex = new FileNotFoundException("XML文件未找到");
                    tcs.SetException(ex);
                }
                else
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);
                    var root = xmlDoc.DocumentElement;
                    var tmpString = JsonConvert.SerializeXmlNode(root, Newtonsoft.Json.Formatting.None, true);
                    var result = JsonConvert.DeserializeObject<T>(tmpString);
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
        /// 从XML文件映射为Json串
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public static Task<string> LoadXMLFileToJsonAsync(string xmlPath)
        {
            var tcs = new TaskCompletionSource<string>();
            try
            {
                if (!File.Exists(xmlPath))
                {
                    var ex = new FileNotFoundException("XML文件未找到");
                    tcs.SetException(ex);
                }
                else
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);
                    var root = xmlDoc.DocumentElement;
                    var json = JsonConvert.SerializeXmlNode(root, Newtonsoft.Json.Formatting.None, true);
                    tcs.SetResult(json);
                }
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
            return tcs.Task;
        }

        /// <summary>
        /// 从实体类映射为XML文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ett"></param>
        /// <param name="xmlPath"></param>
        /// <param name="xmlRoot"></param>
        /// <returns></returns>
        public static Task SaveXMLFileFromEntityAsync<T>(T ett, string xmlPath, string xmlRoot = "root")
        {
            var json = JsonConvert.SerializeObject(ett);
            var xmlDoc = JsonConvert.DeserializeXmlNode(json, xmlRoot, true);
            xmlDoc.Save(xmlPath);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 从Json串映射为XML文件
        /// </summary>
        /// <param name="json"></param>
        /// <param name="xmlPath"></param>
        /// <param name="xmlRoot"></param>
        /// <returns></returns>
        public static Task SaveXMLFileFromJsonAsync(string json, string xmlPath, string xmlRoot = "root")
        {
            var xmlDoc = JsonConvert.DeserializeXmlNode(json, xmlRoot, true);
            xmlDoc.Save(xmlPath);
            return Task.CompletedTask;
        }
    }
}
