using AdminLTE2.Data;
using AdminLTE2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminLTE2.Controllers
{
    public class WarehousesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public WarehousesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var warehouses = _context.warehouses.Include(w => w.Locations).ToList();
            return View(warehouses);
        }

        public IActionResult Create()
        {
            ViewData["customer_id"] = new SelectList(_context.customers, "customer_id", "name");
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouses = _context.warehouses.Find(id);
            if (warehouses == null)
            {
                return NotFound();
            }
            ViewData["location_id"] = new SelectList(_context.locations, "location_id", "address", warehouses.location_id);
            
            return View(warehouses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Warehouses warehouses)
        {

            if (ModelState.IsValid)
            {
                _context.warehouses.AddAsync(warehouses);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Contacto se guardo correctamente";
                return RedirectToAction("Index");
            }

            ViewData["location_id"] = new SelectList(_context.locations, "location_id", "address", warehouses.location_id);
            return View(warehouses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Warehouses warehouses)
        {
            if (id != warehouses.warehouse_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(warehouses);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Contacto se actualizo correctamente";
                return RedirectToAction("Index");
            }
            return View(warehouses);


        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _context.warehouses.Remove(_context.warehouses.Find(id));
            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
