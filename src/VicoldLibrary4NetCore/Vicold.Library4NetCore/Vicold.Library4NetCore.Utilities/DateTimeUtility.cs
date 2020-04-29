using System;

namespace Vicold.Library4NetCore.Utilities
{
    /// <summary>
    /// 日期操作类
    /// </summary>
    public static class DateTimeUtility
    {

        #region 字符串 To DateTime

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
        /// <para>yyMMdd</para>
        /// <para>yyyyMMdd</para>
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
                    case 6:
                        formatter = "yyMMdd";
                        break;
                    case 8:
                        formatter = "yyyyMMdd";
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

        #endregion

        #region DateTime To 字符串 扩展方法

        /// <summary>
        /// 转化为标准日期时间字符格式
        /// <para>yyyy-MM-dd HH:mm:ss</para>
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>标准日期字符格式字符串</returns>
        public static string ToStandardStr(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 转化为日期字符格式
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="delimiter">分隔符,默认“-”</param>
        /// <returns>日期字符格式字符串</returns>
        public static string ToDateStr(this DateTime dateTime, string delimiter = "-")
        {
            return dateTime.ToString($"yyyy{delimiter}MM{delimiter}dd");
        }
        /// <summary>
        /// 转化为时间字符格式
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="delimiter">分隔符,默认“:”</param>
        /// <returns>时间字符格式字符串</returns>
        public static string ToTimeStr(this DateTime dateTime, string delimiter = ":")
        {
            return dateTime.ToString($"HH{delimiter}mm{delimiter}ss");
        }

        #endregion

        #region 时间戳转化

        /// <summary>
        /// 日期转Unix时间戳（到秒）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToUnixTimestamp(DateTime dateTime)
        {
            System.DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local); // 当地时区
            return (long)(dateTime - startTime).TotalSeconds; // 相差秒数
        }
        /// <summary>
        /// Unix时间戳转日期（到秒）
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime FromUnixTimestamp(long unixTimeStamp)
        {
            System.DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local); // 当地时区
            return startTime.AddSeconds(unixTimeStamp);
        }

        /// <summary>
        /// 日期转JavaScript时间戳（到毫秒）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToJSTimestamp(DateTime dateTime)
        {
            System.DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local); // 当地时区
            return (long)(dateTime - startTime).TotalMilliseconds; // 相差毫秒数
        }
        /// <summary>
        /// JavaScript时间戳转日期（到毫秒）
        /// </summary>
        /// <param name="jsTimeStamp"></param>
        /// <returns></returns>
        public static DateTime FromJSTimestamp(long jsTimeStamp)
        {
            System.DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local); // 当地时区
            return startTime.AddMilliseconds(jsTimeStamp);
        }

        #endregion

        #region 日期计算

        /// <summary>
        /// 获取日期是当月的第几周
        /// </summary>
        /// <param name="day">日期时间</param>
        /// <param name="weekStartWithSunday">每周从周日起算，默认false</param>
        /// <returns></returns>
        public static int WeekOfMonth(this DateTime day, bool weekStartWithSunday = false)
        {
            DateTime FirstofMonth;
            FirstofMonth = Convert.ToDateTime(day.Date.Year + "-" + day.Date.Month + "-" + 1);

            int i = (int)FirstofMonth.Date.DayOfWeek;
            if (i == 0)
            {
                i = 7;
            }
            if (!weekStartWithSunday)
            {
                return (day.Date.Day + i - 2) / 7 + 1;
            }
            else
            {
                return (day.Date.Day + i - 1) / 7;
            }
        }

        /// <summary>
        /// 获得两个日期的间隔
        /// </summary>
        /// <param name="dateTime1">日期一</param>
        /// <param name="dateTime2">日期二</param>
        /// <returns>日期间隔TimeSpan。</returns>
        public static TimeSpan GetDateInterval(DateTime dateTime1, DateTime dateTime2)
        {
            TimeSpan ts1 = new TimeSpan(dateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(dateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }

        #endregion
    }
}
