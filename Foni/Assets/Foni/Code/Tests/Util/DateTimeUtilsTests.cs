using System;
using Foni.Code.Tests.Mocks;
using Foni.Code.Util;
using NUnit.Framework;

namespace Foni.Code.Tests.Util
{
    public class DateTimeUtilsTests
    {
        [Test]
        public void TimestampToDateTime()
        {
            const string timestamp = "2023-07-21T09:24:30.000+02:00";

            var result = DateTimeUtils.TimestampToDateTime(timestamp);
            
            Assert.AreEqual(2023, result.Year);
            Assert.AreEqual(7, result.Month);
            Assert.AreEqual(21, result.Day);
            Assert.AreEqual(9, result.Hour);
            Assert.AreEqual(24, result.Minute);
            Assert.AreEqual(30, result.Second);
        }

        [Test]
        public void DateTimeToFriendlyString()
        {
            var mockDateTimeService = MockDateTimeService.Get();

            var result = DateTimeUtils.DateTimeToFriendlyString(mockDateTimeService.Now);

            var localOffset = TimeZoneInfo.Local.GetUtcOffset(mockDateTimeService.Now);

            var expectedUtcDateTime = new DateTime(2023, 6, 30, 15, 30, 45, DateTimeKind.Utc);
            var adjustedDateTime = expectedUtcDateTime + localOffset;
            var expectedString =
                $"{adjustedDateTime.Year}-" +
                $"{adjustedDateTime.Month.ToString().PadLeft(2, '0')}-" +
                $"{adjustedDateTime.Day.ToString().PadLeft(2, '0')} " +
                $"{adjustedDateTime.Hour.ToString().PadLeft(2, '0')}:" +
                $"{adjustedDateTime.Minute.ToString().PadLeft(2, '0')}:" +
                $"{adjustedDateTime.Second.ToString().PadLeft(2, '0')}";

            Assert.AreEqual(expectedString, result);
        }

        [Test]
        public void TimeSpanToFriendlyString()
        {
            var timeSpan = new TimeSpan(0, 1, 30, 45);

            var result = DateTimeUtils.TimeSpanToFriendlyString(timeSpan);

            Assert.AreEqual("90m 45s", result);
        }
    }
}