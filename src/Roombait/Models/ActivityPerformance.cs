using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roombait.Models
{
    public class ActivityPerformance
    {
        [Key]
        public int PerformanceID { get; set; }

        public ApplicationUser User { get; set; }
        public Activity PerformedActivity { get; set; }
        public DateTime WhenPerformed { get; set; }
        public string Memo { get; set; }
    }
}
