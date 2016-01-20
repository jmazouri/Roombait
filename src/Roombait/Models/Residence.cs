using System.Collections.Generic;
using Microsoft.Extensions.CodeGeneration;

namespace Roombait.Models
{
    public class Residence
    {
        public int ResidenceID { get; set; }
        public string Name { get; set; }

        public List<ApplicationUser> Residents { get; set; } 
        public ApplicationUser Owner { get; set; }

        public List<Activity> Activites { get; set; }

        public Residence()
        {
            Activites = new List<Activity>();
        }
    }
}
