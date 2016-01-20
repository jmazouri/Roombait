using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Roombait.Models;

namespace Roombait.Controllers
{
    public class ResidenceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResidenceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            List<Residence> result = null;

            if (User.HasClaim("Admin", "true"))
            {
                result = _context.Residences
                    .Include(d=>d.Residents).ToList();
            }
            else
            {
                result = _context.Residences
                        .Where(residence => residence.Residents.Any(user => user.Id == HttpContext.User.GetUserId()))
                        .Include(d => d.Residents)
                        .ToList();
            }

            return View(new ViewModels.Residence.ResidenceIndexViewModel
            {
                Residences = result
            });
        }

        [Authorize]
        public async Task<IActionResult> View(int id)
        {
            var result = await _context.Residences
                .Include(d=>d.Residents)
                .Include(d=>d.Activites)
                .ThenInclude(d=>d.Performances)
                .SingleOrDefaultAsync(d => d.ResidenceID == id);

            return View(result);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Residence residence)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest();
            }

            var currentUser = _context.Users.First(d => d.Id == User.GetUserId());

            residence.Owner = currentUser;
            residence.Residents.Add(currentUser);

            _context.Residences.Add(residence);
            _context.SaveChanges();

            return new HttpOkResult();
        }

    }
}
