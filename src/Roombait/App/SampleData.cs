using System;
using System.Collections.Generic;
using Microsoft.Framework.DependencyInjection;
using System.Linq;
using Microsoft.AspNet.Identity;
using Roombait.Models;

namespace Roombait.App
{
    public static class SampleData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            UserManager<ApplicationUser> userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            if (context == null) { return; }

            if (!context.Users.Any())
            {
                await userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "jmazouri@gmail.com",
                    Email = "jmazouri@gmail.com",
                    FullName = "John Mazouri"
                }, "Abc123!");
            }

            if (!context.Residences.Any())
            {
                context.Residences.AddRange
                (
                    new Residence
                    {
                        Name = "Room 511",
                        Residents = new List<ApplicationUser>
                        {
                            context.Users.First()
                        }
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

                context.Activities.AddRange
                (
                    new Activity
                    {
                        Name = "Dishes",
                        AssociatedResidence = context.Residences.First(),
                        DaysPerformedList = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday }
                    }
                );

                context.Performances.AddRange
                (
                    new ActivityPerformance
                    {
                        Memo = "Sample Data Memo",
                        PerformedActivity = context.Activities.First(),
                        User = context.Users.First(),
                        WhenPerformed = DateTime.Today.AddDays(-2)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
