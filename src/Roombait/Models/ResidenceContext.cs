using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Roombait.Models
{
    public class ResidenceContext : DbContext
    {
        public DbSet<Residence> Residences { get; set; }
    }

}
