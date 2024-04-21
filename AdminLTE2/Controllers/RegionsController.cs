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
        private readonly int id;

        public RegionsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var regions = _context.regions.AsQueryable();
            return View(regions);
        }
        //---------------------------------------------------------------------------------
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

            //var regions = _context.regions.Find(id);
            var regions = _context.regions.Where(c => c.region_id == id).FirstOrDefault();
            if (regions == null)
            {
                return NotFound();
            }
            return View(regions);
        }
        //------------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Create(Locations locations)
        //public IActionResult Create(Locations locations)
        public IActionResult Create([Bind("region_id,region_name")] Regions regions)
        {

            if (ModelState.IsValid)
            {
                _context.regions.Add(regions);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Contacto se guardo correctamente";
                return RedirectToAction("Index");
            }
            //ViewData["customer_id"] = new SelectList(_context.customers, "customer_id", "name", contacts.customer_id);
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
                return RedirectToAction("Index");
            }
            return View(regions);
        }








            //--------------------------------------------------------------------------------------------------------------------------
            public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var regions = _context.regions.Where(c => c.region_id == id).FirstOrDefault();
            _context.regions.Remove(_context.regions.Find(id));
            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
