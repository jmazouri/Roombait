using System.Collections.Generic;

namespace Roombait.Models
{
    public class Residence
    {
        public int ResidenceID { get; set; }
        public string Name { get; set; }

        public List<ApplicationUser> Residents { get; set; } 
    }
}
