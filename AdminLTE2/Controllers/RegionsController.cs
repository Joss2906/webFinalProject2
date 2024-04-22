using AdminLTE2.Data;
using AdminLTE2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System.Net;

namespace AdminLTE2.Controllers
{
    public class RegionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RegionsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var regions = _context.regions.ToList();
            return View(regions);
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regions = _context.regions.Find(id);
            
            if (regions == null)
            {
                return NotFound();
            }
            return View(regions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Regions regions)
        // public IActionResult Create([Bind("region_id,region_name")] Regions regions)
        {

            if (ModelState.IsValid)
            {
                _context.regions.AddAsync(regions);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "La region se guardo correctamente";
                return RedirectToAction(nameof(Index));
            }
            
            return View(regions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Regions regions)
        {
            //if (id != regions.region_id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                _context.Update(regions);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Contacto se actualizo correctamente";
                return RedirectToAction(nameof(Index));
            }
            return View(regions);
        }
    
    public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var regions = _context.regions.Where(c => c.region_id == id).FirstOrDefault();
            _context.regions.Remove(regions);
            _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
