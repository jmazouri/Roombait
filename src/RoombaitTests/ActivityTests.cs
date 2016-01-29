using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Roombait.Models;
using Xunit;

namespace RoombaitTests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class ActivityTests
    {
        [Fact]
        public void TestDaysPerformedProperty()
        {
            Activity testActivity = new Activity();
            testActivity.DaysPerformedList = new List<DayOfWeek>
            {
                DayOfWeek.Friday,
                DayOfWeek.Thursday,
                DayOfWeek.Wednesday
            };

            Assert.Equal(testActivity.DaysPerformed, "Friday,Thursday,Wednesday");
        }

        [Fact]
        public void TestDaysPerformedEntity()
        {
            Activity testActivity = new Activity("Friday,Thursday,Wednesday");
            Assert.Equal(testActivity.DaysPerformedList, new [] {DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday});
        }

        [Fact]
        public void TestPerformanceCalendar()
        {
            Activity testActivity = new Activity();
            testActivity.DaysPerformedList = new List<DayOfWeek>
            {
                DayOfWeek.Friday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday
            };

            testActivity.Performances = new List<ActivityPerformance>
            {
                new ActivityPerformance
                {
                    WhenPerformed = new DateTime(2016, 6, 7)
                },
                new ActivityPerformance
                {
                    WhenPerformed = new DateTime(2016, 6, 8)
                }
            };

            DateTime fakeToday = new DateTime(2016, 6, 7);

            Assert.Equal(testActivity.PerformanceStatus(fakeToday)[DayOfWeek.Sunday], ActivityState.NotScheduled);
            Assert.Equal(testActivity.PerformanceStatus(fakeToday)[DayOfWeek.Monday], ActivityState.NotScheduled);
            Assert.Equal(testActivity.PerformanceStatus(fakeToday)[DayOfWeek.Tuesday], ActivityState.Completed);
            Assert.Equal(testActivity.PerformanceStatus(fakeToday)[DayOfWeek.Wednesday], ActivityState.Completed);
            Assert.Equal(testActivity.PerformanceStatus(fakeToday)[DayOfWeek.Thursday], ActivityState.NotScheduled);
            Assert.Equal(testActivity.PerformanceStatus(fakeToday)[DayOfWeek.Friday], ActivityState.Upcoming);
            Assert.Equal(testActivity.PerformanceStatus(fakeToday)[DayOfWeek.Saturday], ActivityState.NotScheduled);
        }
    }
}
