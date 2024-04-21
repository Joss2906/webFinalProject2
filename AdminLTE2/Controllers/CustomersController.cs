using AdminLTE2.Data;
using AdminLTE2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminLTE2.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var customers = _context.customers.ToList();
            return View(customers);
        }

        public IActionResult Create()
        {
            //ViewData["customer_id"] = new SelectList(_context.customers, "customer_id", "name");
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = _context.customers.Find(id);
            if (customers == null)
            {
                return NotFound();
            }
            //ViewData["customer_id"] = new SelectList(_context.customers, "customer_id", "name", customers.customer_id);
            return View(customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customers customers)
        {

            if (ModelState.IsValid)
            {
                _context.customers.AddAsync(customers);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Cliente se guardo correctamente";
                return RedirectToAction(nameof(Index));
            }
            //ViewData["customer_id"] = new SelectList(_context.customers, "customer_id", "name", customers.customer_id);
            return View(customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Customers customers)
        {
            if (id != customers.customer_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(customers);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Cliente se actualizo correctamente";
                return RedirectToAction(nameof(Index));
            }
            return View(customers);


        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customers = _context.customers.Find(id);

            _context.customers.Remove(customers);
            _context.SaveChangesAsync();

            return RedirectToAction("Index");

            //return RedirectToAction(nameof(Index));
        }
    
    }
}
