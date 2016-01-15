using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Roombait.Models
{
    public class Activity
    {
        public int ActivityID { get; set; }

        public string Name { get; set; }
        public Residence AssociatedResidence { get; set; }

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
                    .ToList();
            }
            set { DaysPerformed = String.Join(",", value); }
        } 
    }
}
