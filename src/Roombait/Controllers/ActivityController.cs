using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Roombait.Models;

namespace Roombait.Controllers
{
    public class ActivityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivityController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> View(int id)
        {
            var result = await _context.Activities
                .Include(d=>d.AssociatedResidence)
                .Include(d=>d.Performances)
                .ThenInclude(d=>d.User)
                .SingleOrDefaultAsync(d => d.ActivityID == id);

            return View(result);
        }

        [Authorize]
        public IActionResult Perform(int activityId, string memo = "", string dateOverride = "")
        {
            var foundActivity = _context.Activities
                .Include(d => d.Performances)
                .Include(d => d.AssociatedResidence)
                    .ThenInclude(d=>d.Residents)
                .First(d => d.ActivityID == activityId);

            if (foundActivity == null) { return HttpNotFound(); }
            if (foundActivity.AssociatedResidence.Residents.All(d => d.Id != User.GetUserId())) { return HttpUnauthorized(); }

            var currentUser = _context.Users.First(d => d.Id == User.GetUserId());

            DateTime performanceDate = DateTime.Now;

            if (!DateTime.TryParse(dateOverride, out performanceDate))
            {
                performanceDate = DateTime.Now;
            }
            else
            {
                memo = memo + " - Added on " + DateTime.Now.ToString("g");
            }

            foundActivity.Performances.Add(new ActivityPerformance
            {
                User = currentUser,
                WhenPerformed = performanceDate,
                Memo = memo
            });

            _context.SaveChanges();

            return Ok();
        }

        [Authorize]
        public async Task<IActionResult> Data(int id)
        {
            var result = await _context.Activities
                .Include(d => d.Performances)
                .ThenInclude(d=>d.User)
                .SingleOrDefaultAsync(d => d.ActivityID == id);

            var ret = result.Performances.Select(d => new
            {
                title = d.User.Name,
                start = d.WhenPerformed,
                memo = d.Memo
            });

            return Json(ret);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int activityId)
        {
            var foundActivity = _context.Activities
                .Include(d=>d.Performances)
                .Include(d=>d.AssociatedResidence)
                    .ThenInclude(d=>d.Owner)
                .First(d => d.ActivityID == activityId);

            if (User.GetUserId() != foundActivity.AssociatedResidence.Owner.Id)
            {
                return new HttpUnauthorizedResult();
            }

            _context.Performances.RemoveRange(foundActivity.Performances);

            _context.SaveChanges();

            _context.Activities.Remove(foundActivity);

            _context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return View(activity);
            }

            activity.AssociatedResidence =_context.Residences.First(d => d.ResidenceID == Int32.Parse(Request.Form["AssociatedResidence"].First()));
            _context.Activities.Add(activity);
            _context.SaveChanges();

            return RedirectToAction("View", "Residence", new { id = activity.AssociatedResidence.ResidenceID });
        }

    }
}
