using System;

namespace Vicold.Library4NetCore.Utilities
{
    /// <summary>
    /// 日期操作类
    /// </summary>
    public static class DateTimeUtility
    {
        /// <summary>
        /// 使用自定义格式转化为DateTime
        /// </summary>
        /// <param name="dateStr">时间串</param>
        /// <param name="formatter">格式串</param>
        /// <returns></returns>
        public static DateTime ToDateTimeFormString(string dateStr, string formatter)
        {

            if (string.IsNullOrWhiteSpace(dateStr) || string.IsNullOrWhiteSpace(formatter))
            {
                throw new ArgumentNullException("输入时间串不能为空");
            }
            try
            {
                return DateTime.ParseExact(dateStr, formatter, System.Globalization.CultureInfo.CurrentCulture);
            }
            catch
            {
                throw new FormatException("转换失败：输入日期格式不匹配");
            }
        }

        /// <summary>
        /// 使用默认格式转换为DateTime
        /// <para>yyMMddHH</para>
        /// <para>yy-MM-dd HH</para>
        /// <para>yyyyMMddHH</para>
        /// <para>yyyy-MM-dd HH</para>
        /// <para>yyyyMMddHHmm</para>
        /// <para>yyyy-MM-dd HH:mm</para>
        /// <para>yyyyMMddHHmmss</para>
        /// <para>yyyy-MM-dd HH:mm:ss</para>
        /// <para>yyyyMMddHHmmssfff</para>
        /// <para>yyyy-MM-dd HH:mm:ss:fff</para>
        /// </summary>
        /// <param name="dateStr">时间串</param>
        /// <returns></returns>
        public static DateTime ToDateTimeFormString(string dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
            {
                throw new ArgumentNullException("输入时间串不能为空");
            }
            try
            {
                var formatter = "yyyyMMddHHmmss";
                switch (dateStr.Length)
                {
                    case 8:
                        formatter = "yyMMddHH";
                        break;
                    case 11:
                        formatter = "yy-MM-dd HH";
                        break;
                    case 10:
                        formatter = "yyyyMMddHH";
                        break;
                    case 13:
                        formatter = "yyyy-MM-dd HH";
                        break;
                    case 12:
                        formatter = "yyyyMMddHHmm";
                        break;
                    case 16:
                        formatter = "yyyy-MM-dd HH:mm";
                        break;
                    case 14:
                        formatter = "yyyyMMddHHmmss";
                        break;
                    case 19:
                        formatter = "yyyy-MM-dd HH:mm:ss";
                        break;
                    case 17:
                        formatter = "yyyyMMddHHmmssfff";
                        break;
                    case 23:
                        formatter = "yyyy-MM-dd HH:mm:ss:fff";
                        break;
                }
                return ToDateTimeFormString(dateStr, formatter);
            }
            catch
            {
                throw new FormatException("转换失败：输入日期串未匹配到默认日期格式，请使用自定义格式化字符串转换方法");
            }
        }
    }
}
