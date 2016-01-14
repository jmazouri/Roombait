using System;
using Microsoft.Framework.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity.Storage;
using Microsoft.Extensions.OptionsModel;

namespace Roombait.Models
{
    public static class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            if (context == null) { return; }

            if (!context.Residences.Any())
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
            }

            if (!context.Users.Any())
            {

                var user = new ApplicationUser { UserName = "jmazouri@gmail.com", Email = "jmazouri@gmail.com" };
                

                context.Users.Add(user);

                context.SaveChanges();
            }
        }
    }
}
