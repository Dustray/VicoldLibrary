using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Vicold.Library4NetCore.Utilities;

namespace Vicold.UnitTest
{
    [TestClass]
    public class UtilitiesTest
    {
        /// <summary>
        /// 测试颜色类
        /// </summary>
        [TestMethod]
        public void TestColor()
        {
            var c1 = ColorUtility.ToColorFromStr16("#32CD32");
            Assert.AreEqual(c1.ToArgb(), System.Drawing.Color.LimeGreen.ToArgb());
            var c2 = ColorUtility.ToColorFromStr16("#FFD2691E");
            Assert.AreEqual(c2.ToArgb(), System.Drawing.Color.Chocolate.ToArgb());
            var c16 = ColorUtility.ToStr16FromColor(System.Drawing.Color.LightSlateGray);
            Assert.AreEqual(c16, "#778899");
        }

        /// <summary>
        /// 测试日期类
        /// </summary>
        [TestMethod]
        public void TestDateTime()
        {
            //字符串转换
            var now = DateTime.Now.Date.AddHours(8).AddMinutes(7).AddSeconds(6);
            var date1 = DateTimeUtility.ToDateTimeFormString(now.ToString("yyyyMMdd"));
            Assert.AreEqual(date1, now.Date);

            var date2 = DateTimeUtility.ToDateTimeFormString(now.ToString("yyyyMMddHHmmss"));
            Assert.AreEqual(date2, now);

            var date3 = DateTimeUtility.ToDateTimeFormString("2020-02-02 20:20:20", "yyyy-MM-dd HH:mm:ss");
            Assert.AreEqual(date3, new DateTime(2020, 2, 2, 20, 20, 20));
            //时间戳转换
            var unixStamp = 1580646020L;
            var jsStamp = 1580646020000L;
            var date = new DateTime(2020, 2, 2, 20, 20, 20);
            var reDate1 = DateTimeUtility.FromUnixTimestamp(unixStamp);
            Assert.AreEqual(reDate1, date);
            var reDate2 = DateTimeUtility.FromJSTimestamp(jsStamp);
            Assert.AreEqual(reDate2, date);

            var reUnixStamp = DateTimeUtility.ToUnixTimestamp(date);
            Assert.AreEqual(reUnixStamp, unixStamp);
            var reJsStamp = DateTimeUtility.ToJSTimestamp(date);
            Assert.AreEqual(reJsStamp, jsStamp);
        }
    }
}
