using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Vicold.Library4Net.Utilities
{
    /// <summary>
    /// 对象操作类
    /// </summary>
    public static class ObjectUtility
    {
        /// <summary>
        /// 对象克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objSource"></param>
        /// <param name="useMode"></param>
        /// <returns></returns>
        public static T CloneTo<T>(T objSource, CloneMode useMode = CloneMode.Default)
        {
            using (var ms = new MemoryStream())
            {
                if (CloneMode.Default == useMode)
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(ms, objSource);
                    ms.Seek(0, SeekOrigin.Begin);
                    return (T)formatter.Deserialize(ms);
                }
                else if (CloneMode.XMLMode == useMode)
                {
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(ms, objSource);
                    ms.Seek(0, SeekOrigin.Begin);
                    return (T)serializer.Deserialize(ms);
                }
                return default;
            }

        }

        /// <summary>
        /// 集合对象克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToClone"></param>
        /// <returns></returns>
        public static List<T> CloneTo<T>(this List<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }


    }

    public enum CloneMode
    {
        Default,
        XMLMode
    }
}
