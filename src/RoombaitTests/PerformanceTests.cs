using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Roombait.Models;
using Xunit;

namespace RoombaitTests
{
    public class PerformanceTests
    {
        [Theory, MemberData("BetweenDatesTestData")]
        public void BetweenDatesTest(DateTime start, DateTime end)
        {
            ActivityPerformance performance = new ActivityPerformance
            {
                WhenPerformed = new DateTime(2016, 4, 5)
            };

            Assert.True(performance.IsBetweenDates(start, end));
        }

        public static IEnumerable<object[]> BetweenDatesTestData => new[]
        {
            new object[] { new DateTime(2016, 4, 5), new DateTime(2016, 4, 8) },
            new object[] { new DateTime(2016, 4, 2), new DateTime(2016, 4, 5) },
            new object[] { new DateTime(2010, 4, 5), new DateTime(2020, 4, 5) }
        };
    }
}
