using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Roombait.App;
using System.Threading.Tasks;

namespace Roombait.Models
{
    public enum ActivityState
    {
        NotScheduled,
        PastDue,
        Upcoming,
        Completed
    }

    public class Activity
    {
        public int ActivityID { get; set; }

        public string Name { get; set; }
        public Residence AssociatedResidence { get; set; }

        public List<ActivityPerformance> Performances { get; set; } 

        //CSV
        public string DaysPerformed { get; set; }

        [NotMapped]
        public List<DayOfWeek> DaysPerformedList
        {
            get
            {
                var splitDays = DaysPerformed.Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                return splitDays
                    .Select(d=>Enum.Parse(typeof(DayOfWeek), d))
                    .Cast<DayOfWeek>()
                    .OrderBy(d=>d)
                    .ToList();
            }
            set { DaysPerformed = String.Join(",", value); }
        }

        public Activity()
        {
            DaysPerformed = "";
            Performances = new List<ActivityPerformance>();
        }

        [Obsolete("Don't use this constructor unless you have to")]
        public Activity(string daysPerformedCsv) : this()
        {
            DaysPerformed = daysPerformedCsv;
        }

        public Dictionary<DayOfWeek, ActivityState> PerformanceStatus(DateTime relativeTo)
        { 
            Dictionary<DayOfWeek, ActivityState> ret = new Dictionary<DayOfWeek, ActivityState>();

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                ActivityState state = ActivityState.NotScheduled;

                var performancesThisWeek = Performances.Where(d => d.IsBetweenDates(Util.StartOfWeek(relativeTo), Util.EndOfWeek(relativeTo)));

                if (DaysPerformedList.Contains(day))
                {
                    state = day < relativeTo.DayOfWeek ? ActivityState.PastDue : ActivityState.Upcoming;
                }

                if (performancesThisWeek.Any(d => d.WhenPerformed.DayOfWeek == day))
                {
                    state = ActivityState.Completed;
                }

                ret.Add(day, state);
            }

            return ret;
        } 
    }
}
