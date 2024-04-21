using AdminLTE2.Data;
using AdminLTE2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminLTE2.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public InventoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //var inventories = _context.inventories.AsNoTracking().Include(i => i.Products).Include(w => w.Warehouses).Distinct().ToList();
            var inventories = _context.inventories.Include(i => i.Warehouses).Include(i => i.Products).ToList();
            return View(inventories);
        }

        public IActionResult Create()
        {
            ViewData["product_id"] = new SelectList(_context.products, "product_id", "product_name");
            ViewData["warehouse_id"] = new SelectList(_context.warehouses, "warehouse_id", "warehouse_name");
            return View();
        }

        public IActionResult Edit(int? p_id, int? w_id)
        {
            if (p_id == null)
            {
                return NotFound();
            }

            var inventories = _context.inventories
                .Where(i => i.product_id == p_id)
                .Where(i => i.warehouse_id == w_id)
                .FirstOrDefault();

            if (inventories == null)
            {
                return NotFound();
            }

            ViewData["product_id"] = new SelectList(_context.products, "product_id", "product_name");
            ViewData["warehouse_id"] = new SelectList(_context.warehouses, "warehouse_id", "warehouse_name", inventories.warehouse_id);
            return View(inventories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inventories inventories)
        {

            if (ModelState.IsValid)
            {
                _context.inventories.AddAsync(inventories);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Inventario se guardo correctamente";
                return RedirectToAction("Index");
            }

            return View(inventories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Edit([Bind("product_id,warehouse_id,quantity")] Inventories inventories)
        public IActionResult Edit(Inventories inventories)
        {
            //print inventories
            Console.WriteLine(inventories.ToString);
            //if (id != inventories.product_id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                _context.Update(inventories);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Inventario se actualizo correctamente";
                return RedirectToAction("Index");
            }
            //return View(inventories);
            return View();


        }

        public IActionResult Delete(int p_id, int w_id)
        {
            
            var inventories = _context.inventories
                .Where(i => i.product_id == p_id)
                .Where(i => i.warehouse_id == w_id)
                .FirstOrDefault();

            _context.inventories.Remove(inventories);
            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
