using System;
using System.Collections.Generic;
using Microsoft.Framework.DependencyInjection;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Roombait.Models;

namespace Roombait.App
{
    public static class SampleData
    {
        static List<DayOfWeek> RandomDays(Random rand)
        {
            var ret = new List<DayOfWeek>();
            var options = Enum.GetValues(typeof (DayOfWeek)).Cast<DayOfWeek>();

            for (int i = 0; i <= rand.Next(1, options.Count()); i++)
            {
                ret.Add(options.ElementAt(rand.Next(options.Count())));
            }

            return ret;
        }

        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            UserManager<ApplicationUser> userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            Random rand = new Random();

            if (context == null) { return; }

            if (!context.Users.Any())
            {
                await userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "jmazouri@gmail.com",
                    Email = "jmazouri@gmail.com",
                    FullName = "John Mazouri"
                }, "Abc123!");

                await userManager.AddClaimAsync(context.Users.First(d=>d.Email == "jmazouri@gmail.com"), new Claim("Admin", "true"));

                for (int i = 0; i <= 10; i++)
                {
                    await userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = $"asdf{i}@example.com",
                        Email = $"asdf{i}@example.com",
                        FullName = $"Test User {i}"
                    }, "Abc123!");
                }
            }

            if (!context.Residences.Any())
            {
                string[] types = {"Apartment", "Room", "Condo", "House", "Dorm"};

                int maxUsers = context.Users.Count();
                int curCount = 0;

                while (curCount < maxUsers)
                {
                    int taken = rand.Next(0, 4);
                    var users = context.Users.Skip(curCount).Take(taken).ToList();

                    curCount += taken;

                    context.Residences.Add
                    (
                        new Residence
                        {
                            Name = types[rand.Next(types.Length)] + " " + rand.Next(200, 9999),
                            Residents = users.ToList()
                        }
                    );
                }

                context.SaveChanges();
            }

            if (!context.Performances.Any())
            {
                for (int i = 0; i <= 10; i++)
                {
                    context.Performances.Add
                    (
                        new ActivityPerformance
                        {
                            Memo = "Sample Data Memo",
                            User = context.Users.First(),
                            WhenPerformed = DateTime.Today.AddDays(-rand.Next(1, 10))
                        }
                    );
                }

                context.SaveChanges();
            }

            if (!context.Activities.Any())
            {
                string[] types = { "Dishes", "Mop Common Area", "Take out Trash", "Wipe Down Kitchen", "Scrub Toilet", "Cleaning" };

                for (int i = 0; i <= 10; i++)
                {
                    context.Activities.Add
                    (
                        new Activity
                        {
                            Name = types[rand.Next(types.Length)],
                            AssociatedResidence = context.Residences.Skip(rand.Next(context.Residences.Count())).Take(1).First(),
                            DaysPerformedList = RandomDays(rand),
                            Performances = new List<ActivityPerformance>
                            {
                                context.Performances.Skip(i).Take(1).First()
                            }
                        }
                    );
                }

                context.SaveChanges();
            }
            
        }
    }
}

