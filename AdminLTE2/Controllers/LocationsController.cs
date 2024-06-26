﻿using AdminLTE2.Data;
using AdminLTE2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace AdminLTE2.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int id;

        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var locations = _context.locations.Include(l => l.Countries).ToList();
            return View(locations);
        }

        public IActionResult Create()
        {
            ViewData["country_id"] = new SelectList(_context.countries, "country_id", "country_name");
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locations = _context.locations.Find(id);
            if (locations == null)
            {
                return NotFound();
            }
            ViewData["country_id"] = new SelectList(_context.countries, "country_id", "country_name", locations.country_id);
            return View(locations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Locations locations)
        // public IActionResult Create([Bind("location_id,address,postal_code,city,state,country_i")] Locations locations)
        {

            if (ModelState.IsValid)
            {
                _context.locations.Add(locations);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "La direccion se guardo correctamente";
                return RedirectToAction(nameof(Index));
            }
            return View(locations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Locations locations)
        {
            if (id != locations.location_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(locations);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "La direccion se actualizo correctamente";
                return RedirectToAction(nameof(Index));
            }
            return View(locations);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var locations = _context.locations.Where(c => c.location_id == id).FirstOrDefault();
            _context.locations.Remove(locations);
            _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
