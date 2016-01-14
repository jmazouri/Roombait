using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Roombait.Models;

namespace Roombait.Controllers
{
    public class ResidenceController : Controller
    {
        private ResidenceContext _context;

        public ResidenceController(ResidenceContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(new ViewModels.Residence.ResidenceIndexViewModel
            {
                Residences = _context.Residences.ToList()
            });
        }

        [Authorize]
        public async Task<IActionResult> View(int id)
        {
            var result = await _context.Residences.SingleOrDefaultAsync(d => d.ResidenceID == id);
            return View(result);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Residence residence)
        {
            if (!ModelState.IsValid)
            {
                return View(residence);
            }

            _context.Residences.Add(residence);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
