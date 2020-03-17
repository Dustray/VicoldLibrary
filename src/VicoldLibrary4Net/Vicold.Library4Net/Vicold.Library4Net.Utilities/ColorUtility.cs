using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vicold.Library4Net.Utilities
{
    /// <summary>
    /// 时间操作类
    /// </summary>
    public static class ColorUtility
    {

        /// <summary>
        /// 16位字符串转Color对象
        /// <para>#FFFFFF</para>
        /// </summary>
        /// <param name="colorStr16"></param>
        /// <returns></returns>
        public static Color ToColorFromStr16(string colorStr16)
        {
            if (string.IsNullOrWhiteSpace(colorStr16)) return Color.White;
            if (!colorStr16.StartsWith("#")) return Color.White;
            if (colorStr16.Length == 7)
            {
                var colorNewStr = colorStr16.Substring(1, 6);
                try
                {
                    var red = Convert.ToByte(colorNewStr.Substring(0, 2), 16);
                    var green = Convert.ToByte(colorNewStr.Substring(2, 2), 16);
                    var blue = Convert.ToByte(colorNewStr.Substring(4, 2), 16);
                    return Color.FromArgb(255, red, green, blue);
                }
                catch
                {
                    return Color.White;
                }
            }
            else if (colorStr16.Length == 9)
            {
                var colorNewStr = colorStr16.Substring(1, 8);
                try
                {
                    var alpha = Convert.ToByte(colorNewStr.Substring(0, 2), 16);
                    var red = Convert.ToByte(colorNewStr.Substring(2, 2), 16);
                    var green = Convert.ToByte(colorNewStr.Substring(4, 2), 16);
                    var blue = Convert.ToByte(colorNewStr.Substring(6, 2), 16);
                    return Color.FromArgb(alpha, red, green, blue);
                }
                catch
                {
                    return Color.White;
                }
            }
            else
            {
                return Color.White;
            }
        }
    }
}
