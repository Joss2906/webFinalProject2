using AdminLTE2.Data;
using AdminLTE2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminLTE2.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var countries = _context.countries.Include(c => c.Regions).ToList();
            return View(countries);
        }

        public IActionResult Create()
        {
            ViewData["region_id"] = new SelectList(_context.regions, "region_id", "region_name");
            return View();
        }

        public IActionResult Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countries = _context.countries.Where(c => c.country_id == id).FirstOrDefault();
            if (countries == null)
            {
                return NotFound();
            }
            ViewData["region_id"] = new SelectList(_context.regions, "region_id", "region_name", countries.region_id);
            return View(countries);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("country_id,country_name,region_id")] Countries countries)
        {

            if (ModelState.IsValid)
            {
                _context.countries.Add(countries);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Contacto se guardo correctamente";
                return RedirectToAction("Index");
            }
            //ViewData["customer_id"] = new SelectList(_context.customers, "customer_id", "name", contacts.customer_id);
            return View(countries);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Countries countries)
        {
            //if (id != countries.country_id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                _context.Update(countries);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Contacto se actualizo correctamente";
                return RedirectToAction("Index");
            }
            return View(countries);


        }
        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _context.countries.Remove(_context.countries.Find(id));
            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
