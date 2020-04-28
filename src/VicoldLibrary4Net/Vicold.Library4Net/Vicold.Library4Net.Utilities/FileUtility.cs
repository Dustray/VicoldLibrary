using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Vicold.Library4Net.Utilities
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public static class FileUtility
    {
        /// <summary>
        /// 比较文件是否相同（通过Hash值)
        /// </summary>
        /// <param name="filePath1"></param>
        /// <param name="filePath2"></param>
        /// <returns></returns>
        public static bool CompareFileHash(string filePath1, string filePath2)
        {

            using (var hash = HashAlgorithm.Create())
            {
                byte[] buffer1, buffer2;
                //计算第一个文件的哈希值
                using (var fs1 = new FileStream(filePath1, FileMode.Open, FileAccess.Read))
                {
                    buffer1 = hash.ComputeHash(fs1);
                    fs1.Close();
                }
                //计算第二个文件的哈希值
                using (var fs2 = new FileStream(filePath2, FileMode.Open, FileAccess.Read))
                {
                    buffer2 = hash.ComputeHash(fs2);
                    fs2.Close();
                }
                //比较两个哈希值
                return BitConverter.ToString(buffer1) == BitConverter.ToString(buffer2) ? true : false;
            }
        }

        /// <summary>
        /// 获取文件夹中最新的文件
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static FileInfo FindLatestFile(string folderPath, string pattern = null)
        {
            var files = FindAllFile(folderPath, pattern);
            if (null == files || 0 == files.Length) return null;
            files = files.OrderByDescending(v => v.CreationTime).ToArray();
            if (files.Length == 0) return null;
            return files.First();
        }

        /// <summary>
        /// 获取文件夹中所有的文件
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static FileInfo[] FindAllFile(string folderPath, string pattern = null)
        {
            if (!Directory.Exists(folderPath)) return null;
            var root = new DirectoryInfo(folderPath);
            FileInfo[] files;
            if (null == pattern)
                files = root.GetFiles();
            else
                files = root.GetFiles(pattern);
            if (0 == files.Length) return null;
            return files;
        }
    }
}
