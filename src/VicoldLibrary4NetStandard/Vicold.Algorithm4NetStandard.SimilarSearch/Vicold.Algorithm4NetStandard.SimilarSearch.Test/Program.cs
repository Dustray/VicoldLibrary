using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Vicold.Algorithm4NetStandard.SimilarSearch.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"F:\照片\弯弯\IMG_2993.JPG";
            var filePath2 = @"C:\Users\Dustray\Desktop\IMG_2994.JPG";
            var searcher1 = SimilarSearchLoader.Creator();
            var searcher2 = SimilarSearchLoader.Creator();
            string str1;
            string str2;
            byte[] bytes1;
            byte[] bytes2;
            float[,] p1;
            float[,] p2;

            var timewatcher = new Stopwatch();
            var timewatcherAll = new Stopwatch();
            timewatcherAll.Start();
            timewatcher.Start();
            using (var bitmap = new Bitmap(filePath))
            {
                bytes1 = searcher1.GetEigenvalue(bitmap, PoolingType.Mean, precision: 16);
                p1 = searcher1.GetPoolData();
                str1 = System.Text.Encoding.Default.GetString(bytes1);
            }
            SearchPictures(searcher1);
            Console.Read();
            return;
            timewatcher.Stop();
            Console.WriteLine($"计算第一张图片特征值时间：{timewatcher.Elapsed}");
            timewatcher.Restart();

            using (var bitmap = new Bitmap(filePath2))
            {
                bytes2 = searcher2.GetEigenvalue(bitmap, PoolingType.Mean, precision: 16);
                p2 = searcher2.GetPoolData();
                str2 = System.Text.Encoding.Default.GetString(bytes2);
            }
            timewatcher.Stop();
            Console.WriteLine($"计算第二张图片特征值时间：{timewatcher.Elapsed}");
            timewatcher.Restart();

            if (str1 == str2)
            {

            }
            SaveToImage(Path.GetFullPath("p1.bmp"), p1);
            SaveToImage(Path.GetFullPath("p2.bmp"), p2);
            timewatcher.Stop();
            Console.WriteLine($"保存临时图片特征值时间：{timewatcher.Elapsed}");
            timewatcher.Restart();

            var similar = searcher1.GetSimilarity(searcher2);
            timewatcher.Stop();
            Console.WriteLine($"计算相似度时间：{timewatcher.Elapsed}");

            timewatcherAll.Stop();
            Console.WriteLine($"总运行时间：{timewatcherAll.Elapsed}");

            Console.WriteLine($"\r\n相似比例：{similar}");

            Console.Read();
        }

        public static void SearchPictures(ISimilarSearcher sourceSearcher)
        {
            var files = Directory.GetFiles(@"F:\照片\弯弯","*.JPG");
            var index = 0;
            foreach (var file in files)
            {
                var searcher = SimilarSearchLoader.Creator();
                using (var bitmap = new Bitmap(file))
                {
                    _ = searcher.GetEigenvalue(bitmap, PoolingType.Mean, precision: 16);
                    var similar = searcher.GetSimilarity(sourceSearcher);
                    if (similar > 0.75f)
                    {
                        File.Copy(file, Path.GetFullPath($@"output\1\{similar}_{index}.jpg"));
                    }
                    Console.WriteLine($"比较完成第{index}张，共{files.Length}张；相似度：{similar}");
                }
                index++;
            }
        }

        private static void SaveToImage(string filePath, float[,] data)
        {
            var width = data.GetLength(0);
            var height = data.GetLength(1);
            var bit = new Bitmap(width, height);
            var index = 0;
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    //var a = Convert.ToInt32(data[x, y] * 255);
                    //bit.SetPixel(x, y, Color.FromArgb(255, a, a, a));
                    bit.SetPixel(x, y, Color.FromArgb((int)data[x, y]));
                    index++;
                }
            }
            bit.Save(filePath, ImageFormat.Bmp);
            bit.Dispose();
        }

    }
}
