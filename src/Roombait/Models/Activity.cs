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
        private string DaysPerformed { get; set; }

        [NotMapped]
        public List<DayOfWeek> DaysPerformedList
        {
            get
            {
                return DaysPerformed.Split(',')
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

        public Dictionary<DayOfWeek, ActivityState> PerformanceStatus()
        {
            Dictionary<DayOfWeek, ActivityState> ret = new Dictionary<DayOfWeek, ActivityState>();

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                ActivityState state = ActivityState.NotScheduled;

                var performancesThisWeek = Performances.Where(d => d.IsInWeek(Util.StartOfWeek(DateTime.Now), Util.EndOfWeek(DateTime.Now)));

                if (DaysPerformedList.Contains(day))
                {
                    state = day < DateTime.Now.DayOfWeek ? ActivityState.PastDue : ActivityState.Upcoming;
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
