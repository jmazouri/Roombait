using System;
using Microsoft.Framework.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity.Storage;

namespace Roombait.Models
{
    public static class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ResidenceContext>();

            if (context != null && !context.Residences.Any())
            {
                context.Residences.AddRange
                (
                    new Residence
                    {
                        Name = "Room 511"
                    },
                    new Residence
                    {
                        Name = "David's Apartment"
                    },
                    new Residence
                    {
                        Name = "Crapland"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
