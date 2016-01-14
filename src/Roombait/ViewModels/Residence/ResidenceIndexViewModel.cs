using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.WebEncoders;

namespace Roombait.ViewModels.Residence
{
    public class ResidenceIndexViewModel
    {
        public List<Models.Residence> Residences { get; set; }
    }
}
